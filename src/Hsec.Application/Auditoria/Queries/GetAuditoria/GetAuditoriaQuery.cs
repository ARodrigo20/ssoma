using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Auditoria.Models;
using Hsec.Domain.Entities.Auditoria;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.PlanAccion.Queries.GetPlanAccionDocReferencia;

namespace Hsec.Application.Auditoria.Queries.GetAuditoria
{
    
    public class GetAuditoriaQuery : IRequest<GetAuditoriaVM> 
    {
        public string CodAuditoria { get; set; }
        public class GetAuditoriaQueryHandler : IRequestHandler<GetAuditoriaQuery, GetAuditoriaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetAuditoriaQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<GetAuditoriaVM> Handle(GetAuditoriaQuery request, CancellationToken cancellationToken)
            {
                try{
                    var data = new GetAuditoriaVM();

                    var auditoria_T = _context.TAuditoria.Where(
                        t => t.Estado == true 
                        && t.CodAuditoria.Equals(request.CodAuditoria) 
                        )
                        .FirstOrDefault();

                    if(auditoria_T == null) throw new NotFoundException("Auditoria",request.CodAuditoria);


                    // esta linea puede causa error en el colapso de la memeoria si susede duplicar el parametro 
                    //var planAccion = await _planAccion.Get(request.CodAuditoria);
                    var planAccion = await _mediator.Send(new GetPlanAccionDocReferenciaQuery() {DocReferencia = request.CodAuditoria });
                    // foreach(var plan in planAccion){
                    //     foreach(var responsable in plan.responsable){
                            
                    //     }
                    // }

                    data.data = _mapper.Map<TAuditoria,AuditoriaDto>(auditoria_T);

                    //Equipo Auditor
                    data.data.Equipo = _context.TEquipoAuditor
                        .Where(t => t.Estado == true && t.CodAuditoria.Equals(request.CodAuditoria))
                        .ProjectTo<EquipoAuditorDto>(_mapper.ConfigurationProvider)
                        .ToHashSet();

                    data.data.Hallazgos = _context.THallazgos
                        .Where(t => t.Estado == true && t.CodAuditoria.Equals(request.CodAuditoria))
                        .ProjectTo<HallazgoDto>(_mapper.ConfigurationProvider)
                        .ToHashSet();

                    foreach(var itemHallazgo in data.data.Hallazgos){
                        if(itemHallazgo.CodTipoHallazgo == TipoHallazgo.NoConformidad){
                            var item = _context.TAnalisisHallazgo
                                .Where(t => t.Estado == true 
                                    && t.CodHallazgo.Equals(itemHallazgo.CodHallazgo)
                                    && t.CodTipoHallazgo.Equals(itemHallazgo.CodTipoHallazgo)
                                ).FirstOrDefault();
                            itemHallazgo.NoConformidad = _mapper.Map<TAnalisisHallazgo,NoConformidadDto>(item);

                            itemHallazgo.NoConformidad.AnalisisCausalidad = _context.TAuditoriaAnalisisCausalidad
                                .Where(t => t.Estado == true && t.CodHallazgo.Equals(item.CodHallazgo))
                                .ProjectTo<AnalisisCausalidadDto>(_mapper.ConfigurationProvider)
                                .ToHashSet();
                            itemHallazgo.NoConformidad.AnalisisCausalidad = await addInformacionYdescripcion(itemHallazgo.NoConformidad.AnalisisCausalidad);

                        }
                        if(itemHallazgo.CodTipoHallazgo == TipoHallazgo.Observacion){
                            var item = _context.TDatosHallazgo
                                .Where(t => t.Estado == true 
                                    && t.CodHallazgo.Equals(itemHallazgo.CodHallazgo)
                                    && t.CodTipoHallazgo.Equals(itemHallazgo.CodTipoHallazgo)
                                ).FirstOrDefault();
                            itemHallazgo.Observacion = _mapper.Map<TDatosHallazgo,DatosHallazgoDto>(item);
                        }
                        if(itemHallazgo.CodTipoHallazgo == TipoHallazgo.OportunidadMejora){
                            var item = _context.TDatosHallazgo
                                .Where(t => t.Estado == true 
                                    && t.CodHallazgo.Equals(itemHallazgo.CodHallazgo)
                                    && t.CodTipoHallazgo.Equals(itemHallazgo.CodTipoHallazgo)
                                ).FirstOrDefault();
                            itemHallazgo.OportunidadMejora = _mapper.Map<TDatosHallazgo,DatosHallazgoDto>(item);
                        }
                        if(itemHallazgo.CodTipoHallazgo == TipoHallazgo.RequiereCorreccion){
                            var item = _context.TDatosHallazgo
                                .Where(t => t.Estado == true 
                                    && t.CodHallazgo.Equals(itemHallazgo.CodHallazgo)
                                    && t.CodTipoHallazgo.Equals(itemHallazgo.CodTipoHallazgo)
                                ).FirstOrDefault();
                            itemHallazgo.RequiereCorreccion = _mapper.Map<TDatosHallazgo,DatosHallazgoDto>(item);
                        }

                        itemHallazgo.planAccion = planAccion.data.Where(t => t.docSubReferencia.Equals(itemHallazgo.CodHallazgo)).ToList();
                    }

                    TAudCartilla AudCC = _context.TAudCartilla.Where(t => t.Estado == true && t.CodAuditoria.Equals(request.CodAuditoria)).FirstOrDefault();
                    if(AudCC!=null){
                        data.data.ControlCritico = _mapper.Map<TAudCartilla,ControlCriticoDto>(AudCC);
                        data.data.ControlCritico.Criterios = _context.TAudCCCriterio
                            .Where(t => t.Estado == true && t.CodAuditoria.Equals(request.CodAuditoria) && t.CodCartilla.Equals(AudCC.CodCartilla))
                            .ProjectTo<CCCriteriosDto>(_mapper.ConfigurationProvider)
                            .ToHashSet();
                    }

                    return data;
                }catch(Exception e){
                    var ee = e;
                    throw e;
                }
            }

            private async Task<ICollection<AnalisisCausalidadDto>> addInformacionYdescripcion(ICollection<AnalisisCausalidadDto> analisisCausalidad)
            {
                //foreach(var item in analisisCausalidad){
                //    item.DesCausa = await _generalService.GetAnalisisCausa(item.CodCausa);
                //    item.DesCondicion =  await _generalService.GetAnalisisCausa(item.CodCausa+"."+item.CodCondicion);
                //    item.DesAnalisisCausa =  await _generalService.GetAnalisisCausa(item.CodCausa+"."+item.CodCondicion+"."+item.CodAnalisisCausa);
                //}
                return analisisCausalidad;
            }
        }

    }
}