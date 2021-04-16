using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Domain.Entities.General;
using Hsec.Application.Incidentes.Queries.GetCodigoPosicion;

namespace Hsec.Application.General.Aprobaciones.Commands.InciarAprobacion
{
    public class IniciarAprobacionCommand : IRequest<int>
    {
        public string CodResponsable { get; set; }
        public string DocReferencia { get; set; }
        public string CodTabla { get; set; }
        //public List<string> Aprobadores { get; set; }
        //public int Version { get; set; }
        public class IniciarAprobacionCommandHandler : IRequestHandler<IniciarAprobacionCommand, int>
        {
            private readonly IApplicationDbContext _context;
            //private readonly IDocReferencia _dataDocRef;
            private readonly IConfiguration _config;
            private readonly ICorreosService _sendCorreo;

            public IniciarAprobacionCommandHandler(IApplicationDbContext context, /*IDocReferencia dataDocRef,*/ IConfiguration config, ICorreosService sendCorreo)//  , 
            {
                _context = context;
                //_dataDocRef = dataDocRef;
                _config = config;
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
                return String.Join('.', nuevaCadena);
            }
            public async Task<int> Handle(IniciarAprobacionCommand request, CancellationToken cancellationToken)
            {               
                var version = 1;
                var validate_old = _context.TAprobacion.Where(t => t.DocReferencia.Equals(request.DocReferencia)).OrderByDescending(t => t.Creado).FirstOrDefault();
                if(validate_old != null) 
                    if(validate_old.EstadoDoc == "P" || validate_old.EstadoDoc.Equals(APROBAR) || validate_old.EstadoDoc.Equals(RECHAZAR)){
                        throw new ArgumentException("documento en uso");
                    }
                    else{
                        if(validate_old.EstadoDoc == MODIFICAR) version = validate_old.Version+1;
                        else throw new ArgumentException("Estado de documento no valido");
                    }

                var aprobar = new TAprobacion();
                aprobar.Version = version;
                aprobar.EstadoIncial(request.DocReferencia,request.CodTabla,request.CodResponsable,"P");
                var cadenaAprobacionPosicion = _context.TProceso.Where(t=> t.CodTabla== request.CodTabla).Select(p=>p.CadenaAprobacion).FirstOrDefault();

                if (cadenaAprobacionPosicion != null) {
                    var cadenaApro =await plantillaToPosicion(cadenaAprobacionPosicion,request.CodTabla,request.DocReferencia,request.CodResponsable);
                    aprobar.ProcesoAprobacion = cadenaApro;
                    var codJerRes = _context.TJerarquiaPersona.Where(jp => jp.CodPersona == request.CodResponsable).First();                   
                    while (peek(cadenaApro).Contains(request.CodResponsable) || peek(cadenaApro).Contains(codJerRes.CodPosicion + "")) {
                        var ha = new THistorialAprobacion()
                        {
                         //   CodAprobacion = aprobar.CodAprobacion,
                            Comentario = "Aprobación Automatica",
                            CodAprobador = request.CodResponsable,
                            EstadoAprobacion = APROBAR
                        };
                        aprobar.Historial.Add(ha);
                        cadenaApro = pop(cadenaApro);
                    }
                    aprobar.CadenaAprobacion = cadenaApro;                    

                    if (cadenaApro.Equals(""))
                    {
                        aprobar.EstadoDoc = "A";
                        _context.TAprobacion.Add(aprobar);
                        var save = await _context.SaveChangesAsync(cancellationToken);

                        var correo = _context.TPersona.Find(request.CodResponsable).Email;
                        _sendCorreo.NotificarDocAprobado(correo, request.DocReferencia, request.CodTabla);
                    }
                    else {

                        _context.TAprobacion.Add(aprobar);
                        var save = await _context.SaveChangesAsync(cancellationToken);

                        var aprobador = peek(aprobar.CadenaAprobacion);
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
                         _sendCorreo.NotificarAprobador(correos, request.DocReferencia, request.CodTabla);
                    }               
                }                
                
                return aprobar.CodAprobacion;
            }

            private async Task<string> plantillaToPosicion(string cadenaAprobacion,string CodTabla,string DocReferencia,string CodResponsable)
            {
                var newCadena = "";
                var Plantillas = _context.TMaestro.Where(m => m.CodTabla == "PlantillaAprobacion").ToList();
                if (CodTabla == "TINC") {

                    //var incidente = await _dataDocRef.getIncidente(DocReferencia);
                    
                    var incidente = _context.TIncidente.Where(t => t.CodIncidente.Equals(DocReferencia)).FirstOrDefault();


                    //var list = cadenaAprobacion.Split('.');                    
                    foreach (var item in cadenaAprobacion.Split('.'))
                    {
                        var Tipo = Plantillas.Where(m => m.CodTipo == item).Select(m => m.DescripcionCorta).FirstOrDefault();
                        if (Tipo == "G") newCadena = "J-"+incidente.CodPosicionGer;
                        else if (Tipo == "S" && incidente.CodPosicionSup != "") newCadena += ".J-" + incidente.CodPosicionSup;
                        else if (item == "R001") {
                            int CodPosicion = 0;
                            if (incidente.CodPosicionSup != "") CodPosicion = Convert.ToInt32(incidente.CodPosicionSup);
                            else CodPosicion = Convert.ToInt32(incidente.CodPosicionGer);
                            int TipoArea = 0;
                            if (incidente.CodAreaHsec == "003") TipoArea = 2;
                            else TipoArea = 1;
                            CodResponsable = _context.TJerarquiaResponsable.Where(jr => jr.CodPosicion == CodPosicion && (jr.CodTipo == TipoArea || jr.CodTipo == 0)).Select(p => p.CodPersona).FirstOrDefault();
                            if (CodResponsable == null) {
                                CodPosicion= Convert.ToInt32(_config["appSettings:NodoRaiz"]);
                                CodResponsable =_context.TJerarquiaResponsable.Where(jr => jr.CodPosicion == CodPosicion && (jr.CodTipo == TipoArea || jr.CodTipo == 0)).Select(p => p.CodPersona).FirstOrDefault();
                            }
                            newCadena += ".P-" + CodResponsable;
                        }
                    }
                }
                else //f (CodTabla == "TPLAN") Caso Generico cuando envia el CodResponasble
                {
                    var pathJerquia = _context.TJerarquiaPersona.Include(t => t.Jerarquia).Where(t => t.CodPersona.Equals(CodResponsable)).FirstOrDefault();
                    if(pathJerquia == null) throw new ArgumentException("Rersponsable no tiene Jeraquia");
                    // lista de posicion de jeraruias superiores de la persona
                    List<Tuple<int?,string>> JerarquiaPersona= new List<Tuple<int?, string>>();
                    foreach (string item in pathJerquia.Jerarquia.PathJerarquia.Split("/")) //- /3553/3562/3563/3564/
                    {
                        if (item.Length==0 ) continue;// || item == pathJerquia.CodPosicion+""
                        int CodPosicion = Convert.ToInt32(item);
                        var jerPost = _context.TJerarquia.Where(j => j.CodPosicion == CodPosicion && j.Visible).FirstOrDefault();
                        if (jerPost == null || jerPost.CodPosicion == Convert.ToInt32(_config["appSettings:NodoRaiz"])) continue; // evitamos nullos y el gerente general

                        if (jerPost.Tipo == "O" && jerPost.Cargo.Contains("SUPERVISOR")) JerarquiaPersona.Add(new Tuple<int?, string>(jerPost.CodPosicion, "O"));
                        else if (jerPost.Tipo == "S" && jerPost.Cargo.Contains("SUPERINTENDENTE")) JerarquiaPersona.Add(new Tuple<int?, string>(jerPost.CodPosicion, "S"));
                        else if (jerPost.Tipo == "G" && jerPost.Cargo.Contains("SENIOR")) JerarquiaPersona.Add(new Tuple<int?, string>(jerPost.CodPosicion, "GS"));
                        else if (jerPost.Tipo == "G" && jerPost.Cargo.Contains("GERENTE")) JerarquiaPersona.Add(new Tuple<int?, string>(jerPost.CodPosicion, "G"));                        
                    }
                    if(JerarquiaPersona.Count==0) throw new ArgumentException("Rersponsable no tiene Jeraquia Valida");
                    //foreach(var item in cadenaAprobacion.Split('.'))
                    var cadenaAnt = cadenaAprobacion.Split('.');
                    for (var i =0; i< cadenaAnt.Length;i++)
                    {
                        var Tipo = Plantillas.Where(m => m.CodTipo == cadenaAnt[i]).Select(m => m.DescripcionCorta).FirstOrDefault();
                        var CodPosicion = JerarquiaPersona.Where(jp => jp.Item2 == Tipo).Select(t => t.Item1).FirstOrDefault();
                        newCadena = i == 0 ? "J-"+CodPosicion : newCadena + ".J-" + CodPosicion;
                    }
                                              
                }
                return newCadena;
            }
        }
    }
}