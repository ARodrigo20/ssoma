using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;
//using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace Hsec.Application.General.Aprobaciones.Commands.Aprobacion
{
    public class AprobacionCommand : IRequest<Unit>
    {       
        public string CodAprobador { get; set; }
        public string Comentario { get; set; }
        public string DocReferencia { get; set; }
        public string EstadoAprobacion { get; set; }
        public string CodTabla { get; set; }

        public class AprobacionCommandHandler : IRequestHandler<AprobacionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;
            private readonly ICorreosService _sendCorreo;

            public AprobacionCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContext, ICorreosService sendCorreo)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
                _sendCorreo = sendCorreo;
            }

            public const string APROBAR = "A";
            public const string RECHAZAR = "R";
            public const string MODIFICAR = "M";
            private string peek(string cadenaAprobacion)
            {
                var list = cadenaAprobacion.Split('.');
                var apro = list.LastOrDefault();
                return apro;
            }
            private string pop(string cadenaAprobacion)
            {
                var list = cadenaAprobacion.Split('.');
                var nuevaCadena = list.SkipLast(1).ToList();
                return String.Join('.',nuevaCadena);
            }
            private string usuarioToCodPersona()
            {                
                return _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }
            private THistorialAprobacion nuevoHistorial(int codAprobacion,string comentario,string codAprobador,string estadoAprobacion)
            {
                var ha = new THistorialAprobacion(){
                    CodAprobacion = codAprobacion,
                    Comentario = comentario, 
                    CodAprobador = codAprobador, 
                    EstadoAprobacion= estadoAprobacion};
                return ha;
            }
            private bool validarPosicion(string codPosicion, string codPersona)
            {                
                var tjer = _context.TJerarquiaPersona.Where(t => t.CodPosicion.Equals(codPosicion)&& t.Estado==true && t.CodPersona== codPersona);
                if (tjer == null || tjer.Count() == 0) return false;
                else return true;
            }
            private void enviarCorreos(string aprobador, string DocReferencia, string CodTabla)
            {
                var correos = "";
                if (aprobador.Contains("J-"))
                {
                    int CodPosicion = Convert.ToInt32(aprobador.Replace("J-", ""));
                    var correo = _context.TJerarquiaPersona.Join(_context.TPersona, jer => jer.CodPersona, per => per.CodPersona, (jer, per) => new { jer = jer, per = per })
                        .Where(tuple => (tuple.jer.CodPosicion == CodPosicion && tuple.jer.CodTipoPersona == 1 && tuple.jer.FechaInicio < DateTime.Now && tuple.jer.FechaFin > DateTime.Now)).Select(p => p.per.Email);
                    correos = string.Join(";", correo);
                }
                else
                {
                    correos = _context.TPersona.Find(aprobador.Replace("P-", "")).Email;
                }
                _sendCorreo.NotificarAprobador(correos, DocReferencia, CodTabla);
            }
            private void validar(string codigo){
                string user = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (user.Equals("admin")) return;
                var list = codigo.Split('-');
                if (list == null || list.Count() != 2) throw new Exception("cadena no valida");
                var codPersona = usuarioToCodPersona();
                if(list[0].ToUpper().Equals("J")) {
                    if(!validarPosicion(list[1],codPersona)) throw new Exception("error persona no tiene permisos de validar esta operacion");
                }
                else if(list[0].ToUpper().Equals("P")){
                    if(list[1]!=codPersona) throw new Exception("error persona no tiene permisos de validar esta operacion");
                }
                else {
                    throw new Exception("cadena no valida");
                }
                
            }
            public async Task<Unit> Handle(AprobacionCommand request, CancellationToken cancellationToken){
                var data = request;
                if(!(data.EstadoAprobacion == null || request.EstadoAprobacion.Equals(APROBAR) || data.EstadoAprobacion.Equals(RECHAZAR) || data.EstadoAprobacion.Equals(MODIFICAR)))
                    throw new ArgumentException("Estado no valido");
                var aprobacion = _context.TAprobacion.Where(t => t.DocReferencia.Equals(data.DocReferencia)).OrderByDescending(t => t.Creado).FirstOrDefault();
                if(aprobacion.EstadoDoc!="P") throw new Exception("Este documento ya fue " + Getdescripcion(aprobacion.EstadoDoc));
                var codPosicion = peek(aprobacion.CadenaAprobacion);
                
                validar(codPosicion);
                
                aprobacion.CadenaAprobacion = pop(aprobacion.CadenaAprobacion);
                if (aprobacion.CadenaAprobacion.Equals("") && data.EstadoAprobacion == APROBAR)
                {
                    aprobacion.EstadoDoc = data.EstadoAprobacion;
                    var correo = _context.TPersona.Find(aprobacion.CodResponsable).Email;
                    _sendCorreo.NotificarDocAprobado(correo, data.DocReferencia, data.CodTabla);
                }
                else if (data.EstadoAprobacion == RECHAZAR)
                {
                    aprobacion.EstadoDoc = data.EstadoAprobacion;
                    var correo = _context.TPersona.Find(aprobacion.CodResponsable).Email;
                    _sendCorreo.NotificarRechazo(correo, data.DocReferencia, data.CodTabla);
                }
                else if (data.EstadoAprobacion == MODIFICAR)
                {
                    aprobacion.EstadoDoc = data.EstadoAprobacion;
                    var correo = _context.TPersona.Find(aprobacion.CodResponsable).Email;
                    _sendCorreo.NotificarModificar(correo, data.DocReferencia, data.CodTabla);
                }
                else {
                    var codJerResp = _context.TJerarquiaPersona.Where(jp => jp.CodPersona == aprobacion.CodResponsable).First();
                    while (peek(aprobacion.CadenaAprobacion).Contains(aprobacion.CodResponsable) || peek(aprobacion.CadenaAprobacion).Contains(codJerResp.CodPosicion + ""))
                    {
                        var ha = new THistorialAprobacion()
                        {
                            CodAprobacion = aprobacion.CodAprobacion,
                            Comentario = "Aprobación Automatica",
                            CodAprobador = aprobacion.CodResponsable,
                            EstadoAprobacion = APROBAR
                        };
                        aprobacion.Historial.Add(ha);
                        aprobacion.CadenaAprobacion = pop(aprobacion.CadenaAprobacion);
                    }
                    if (aprobacion.CadenaAprobacion.Equals("")) {
                        aprobacion.EstadoDoc = "A";
                        var correo = _context.TPersona.Find(aprobacion.CodResponsable).Email;
                        _sendCorreo.NotificarDocAprobado(correo, data.DocReferencia, data.CodTabla);
                    }
                    else enviarCorreos(peek(aprobacion.CadenaAprobacion), data.DocReferencia, data.CodTabla);
                }
                var HistorialAprobacion = nuevoHistorial(aprobacion.CodAprobacion, data.Comentario,usuarioToCodPersona(),data.EstadoAprobacion.ToString());
                aprobacion.Historial.Add(HistorialAprobacion);

                _context.TAprobacion.Update(aprobacion);
                await _context.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
            public string Getdescripcion(string estado)
            {
                if (estado == "R") return "Rechazado";
                if (estado == "A") return "Aprobado";
                if (estado == "M") return "Modificado";
                if (estado == "P") return "Pendiente";
                return "";
            }
        }
    }
}