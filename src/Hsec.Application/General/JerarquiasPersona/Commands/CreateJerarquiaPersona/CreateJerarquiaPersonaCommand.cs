using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Domain.Entities.General;
using System;

namespace Hsec.Application.General.JerarquiasPersona.Commands.CreateJerarquiaPersona
{
    public partial class CreateJerarquiaPersonaCommand : IRequest
    {
        public int CodPosicion { get; set; }
        public string CodPersona { get; set; }
        public int CodTipoPersona { get; set; }
        public string CodElipse { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Asignacion { get; set; }

        public class CreateJerarquiasPersonaCommandHandler : IRequestHandler<CreateJerarquiaPersonaCommand>
        {
            private readonly IApplicationDbContext _context;

            public CreateJerarquiasPersonaCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateJerarquiaPersonaCommand request, CancellationToken cancellationToken)
            {
                //var entity = await _context.TJerarquiaPersona.FindAsync(request.CodPosicion, request.CodPersona);

                //if (entity == null)
                //{
               
                    var entity = new TJerarquiaPersona();
                    entity.Jerarquia = null;
                   // entity.Persona = null;
                    entity.CodPosicion = request.CodPosicion;                    
                    entity.CodPersona = request.CodPersona;                   
                    entity.CodTipoPersona = request.CodTipoPersona;
                    entity.CodElipse = request.CodElipse;
                    entity.FechaInicio = request.FechaInicio;
                    entity.FechaFin = request.FechaFin;
                    entity.PosicionPrimaria = request.Asignacion;
                    _context.TJerarquiaPersona.Add(entity);
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(true);                                     
                    return Unit.Value;
                //}

                //if (entity.Estado == false) {
                //    entity.FechaInicio = request.FechaInicio;
                //    entity.FechaFin = request.FechaFin;
                //    entity.PosicionPrimaria = request.Asignacion;
                //    entity.CodTipoPersona = request.CodTipoPersona;
                //    entity.Estado = true;
                //    _context.TJerarquiaPersona.Update(entity);                   
                //}

            }
        }
    }
}


