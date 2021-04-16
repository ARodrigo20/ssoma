using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Aprobaciones.Commands.SendNotificacion
{
    public class SendNotificacionCommand : IRequest<Unit>
    {
        public string DocReferencia { get; set; }
        public string CodTabla { get; set; }

        public class SendNotificacionCommandHandler : IRequestHandler<SendNotificacionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICorreosService _sendCorreo;

            public SendNotificacionCommandHandler(IApplicationDbContext context, ICorreosService sendCorreo)
            {
                _context = context;
                _sendCorreo= sendCorreo;
        }
            private string peek(string cadenaAprobacion)
            {
                var list = cadenaAprobacion.Split('.');
                var apro = list.LastOrDefault();
                return apro;
            }
            public async Task<Unit> Handle(SendNotificacionCommand request, CancellationToken cancellationToken)
            {
                var aprobar = _context.TAprobacion.Where(t => t.DocReferencia.Equals(request.DocReferencia)).OrderByDescending(t => t.Creado).FirstOrDefault();

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

                return Unit.Value;
            }
        }
    }
}
