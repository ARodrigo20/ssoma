using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Files.Commands.DeleteFileDocRef;
using Hsec.Application.PlanAccion.Commands.DeleteDocRefHsec;

namespace Hsec.Application.Incidentes.Commands.DeleteIncidente
{

    public class DeleteIncidenteCommand : IRequest<int>
    {        
        public string Codigo { get; set; }
        public class DeleteIncidenteCommandHandler : IRequestHandler<DeleteIncidenteCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public DeleteIncidenteCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<int> Handle(DeleteIncidenteCommand request, CancellationToken cancellationToken)
            {
              
                    
                string COD_INCIDENTE = request.Codigo;

                TIncidente incidente = _context.TIncidente.Find(COD_INCIDENTE);
                if(incidente==null || incidente.Estado == false) throw new NotFoundException("incidente",COD_INCIDENTE);
                incidente.Estado = false;
                _context.TIncidente.Update(incidente);

                //ICAM
                incidente.Ticam = _context.TIcam.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                foreach (var item in incidente.Ticam) item.Estado = false;
                _context.TIcam.UpdateRange(incidente.Ticam);

                //analisis Causa
                incidente.TincidenteAnalisisCausa = _context.TIncidenteAnalisisCausa.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                foreach (var item in incidente.TincidenteAnalisisCausa) item.Estado = false;
                _context.TIncidenteAnalisisCausa.UpdateRange(incidente.TincidenteAnalisisCausa);

                //detalle
                incidente.TequipoInvestigacion = _context.TEquipoInvestigacion.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                foreach (var item in incidente.TequipoInvestigacion) item.Estado = false;
                _context.TEquipoInvestigacion.UpdateRange(incidente.TequipoInvestigacion);
                incidente.TsecuenciaEvento = _context.TSecuenciaEvento.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                foreach (var item in incidente.TsecuenciaEvento) item.Estado = false;
                _context.TSecuenciaEvento.UpdateRange(incidente.TsecuenciaEvento);
                incidente.TtestigoInvolucrado = _context.TTestigoInvolucrado.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                foreach (var item in incidente.TtestigoInvolucrado) item.Estado = false;
                _context.TTestigoInvolucrado.UpdateRange(incidente.TtestigoInvolucrado);

                //afectados
                var DetalleAfectado = _context.TDetalleAfectado.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).First();
                DetalleAfectado.Estado = false;
                _context.TDetalleAfectado.Update(DetalleAfectado);

                incidente.TafectadoComunidad = _context.TAfectadoComunidad.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                foreach (var item in incidente.TafectadoComunidad) item.Estado = false;
                _context.TAfectadoComunidad.UpdateRange(incidente.TafectadoComunidad);

                incidente.TafectadoMedioAmbiente = _context.TAfectadoMedioAmbiente.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                foreach (var item in incidente.TafectadoMedioAmbiente) item.Estado = false;
                _context.TAfectadoMedioAmbiente.UpdateRange(incidente.TafectadoMedioAmbiente);

                incidente.TafectadoPropiedad = _context.TAfectadoPropiedad.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                foreach (var item in incidente.TafectadoPropiedad) item.Estado = false;
                _context.TAfectadoPropiedad.UpdateRange(incidente.TafectadoPropiedad);

                incidente.TinvestigaAfectado = _context.TInvestigaAfectado.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                foreach (var item in incidente.TinvestigaAfectado) item.Estado = false;
                _context.TInvestigaAfectado.UpdateRange(incidente.TinvestigaAfectado);

                incidente.TdiasPerdidosAfectado = _context.TDiasPerdidosAfectado.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                foreach (var item in incidente.TdiasPerdidosAfectado) item.Estado = false;
                _context.TInvestigaAfectado.UpdateRange(incidente.TinvestigaAfectado);

                //var ri = _imagen.Delete(COD_INCIDENTE);
                var ri = await _mediator.Send(new DeleteFileDocRefCommand() { NroDocReferencia = COD_INCIDENTE });

                //var rplan = _planAccion.Delete(COD_INCIDENTE);
                var rplan = await _mediator.Send(new DeleteDocRefCommand() { NroDocReferencia = COD_INCIDENTE });

                return await _context.SaveChangesAsync(cancellationToken);


               

            }
        }

    }
}