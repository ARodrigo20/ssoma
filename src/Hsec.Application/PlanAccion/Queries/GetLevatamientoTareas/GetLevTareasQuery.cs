using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.PlanAccion.Commands.CreateLevantamientoTareas;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;
using Hsec.Application.PlanAccion.Queries.GetPlanDeAccion;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;
using Hsec.Application.General.Personas.Queries.GetCode2Name;

namespace Hsec.Application.PlanAccion.Queries.GetLevatamientoTareas
{
    public class GetLevTareasQuery : IRequest<PlanCompVM>
    {
        public int codAccion { get; set; }
        public class GetLevTareasQueryHandler : IRequestHandler<GetLevTareasQuery, PlanCompVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetLevTareasQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<PlanCompVM> Handle(GetLevTareasQuery request, CancellationToken cancellationToken)
            {              
                var codAccion = request.codAccion;
                var planAccionList = _context.TAccion.Where(i => i.CodAccion == codAccion && i.Estado);

                if (planAccionList != null)
                {
                    var plan = planAccionList.AsQueryable()
                    .ProjectTo<PlanRespVM>(_mapper.ConfigurationProvider)
                    .ToList();

                    PlanCompVM planVM = new PlanCompVM();
                    
                    foreach (var item in plan)
                    {
                        if (item.estado)
                        {
                            PlanRespVM planRespVM = new PlanRespVM();
                            planRespVM.codAccion = item.codAccion;
                            planRespVM.codEstadoAccion = item.codEstadoAccion;                            
                            planRespVM.estado = item.estado;
                            planRespVM.codActiRelacionada = item.codActiRelacionada;
                            planRespVM.codAreaHsec = item.codAreaHsec;
                            planRespVM.codNivelRiesgo = item.codNivelRiesgo;
                            planRespVM.codTipoAccion = item.codTipoAccion;
                            planRespVM.codSolicitadoPor = item.codSolicitadoPor;
                            //var _data = await _persons.RequestNombApellidos(item.codSolicitadoPor);
                            var _data = await _mediator.Send(new GetPersonaQuery() { CodPersona = item.codSolicitadoPor });
                            planRespVM.nombreSolicitadoPor = (_data != null) ? _data.Nombres + " " + _data.ApellidoPaterno + " " + _data.ApellidoMaterno : "";
                            planRespVM.docReferencia = item.docReferencia;
                            planRespVM.docSubReferencia = item.docSubReferencia;

                            planRespVM.fechaSolicitud = item.fechaSolicitud;
                            planRespVM.tarea = item.tarea;
                            planRespVM.fechaInicial = item.fechaInicial;
                            planRespVM.fechaFinal = item.fechaFinal;
                            planRespVM.codTablaRef = item.codTablaRef;

                            planRespVM.RespPlanAccion = item.RespPlanAccion.Where(i => i.estado).ToList();

                            foreach (var it in planRespVM.RespPlanAccion) {

                                if (it.estado) {
                                    PersonaVM respVM = new PersonaVM();
                                    if (!String.IsNullOrEmpty(it.codPersona))
                                    {
                                        //respVM = await _persons.RequestNombApellidos(it.codPersona);
                                        respVM = await _mediator.Send(new GetPersonaQuery() { CodPersona = it.codPersona });
                                    }
                                    if (respVM != null)
                                    {
                                        it.nombres = respVM.Nombres;
                                        it.apellidoPaterno = respVM.ApellidoPaterno;
                                        it.apellidoMaterno = respVM.ApellidoMaterno;
                                    }
                                    else
                                    {
                                        it.nombres = "";
                                        it.apellidoPaterno = "";
                                        it.apellidoMaterno = "";
                                    }
                                    var _registros = item.registros.Where(i => i.codAccion == it.codAccion && i.codPersona == it.codPersona);
                                    if(_registros != null && _registros.Count() > 0)
                                    {
                                        it.porcentajeMayor = _registros.Max(i => i.porcentajeAvance);
                                    }

                                    //it.porcentajeMayor = item.registros.Where(i => i.codAccion == it.codAccion && i.codPersona == it.codPersona).Max(i => i.porcentajeAvance);
                                }
                            }

                            var registros = item.registros.Where(i => i.estado).ToList();
                            foreach (var it in registros)
                            {
                                if (it.estado)
                                {
                                    List<String> nomb = new List<string>();
                                    nomb.Add(it.codPersona);
                                    string nombres = "";
                                    //List<String> nombPerson = await _persons.Request(nomb);
                                    List<String> nombPerson = await _mediator.Send(new GetCode2NameQuery() { data = nomb });
                                    if (nombPerson.Count() != 0) {
                                        nombres = nombPerson[0];
                                    }
                                    
                                    //var filesEntity = _context.TFile.Where(i => i.NroDocReferencia == it.correlativo.ToString() && i.Estado).ToList();
                                    var filesEntity = _context.TFile.Where(i => i.NroDocReferencia == it.codAccion.ToString() && i.NroSubDocReferencia == it.correlativo.ToString() && i.Estado).ToList();
                                    var filesVm = filesEntity.AsQueryable()
                                    .ProjectTo<FilesDto>(_mapper.ConfigurationProvider)
                                    .ToList();
                                    it.nombres = nombres;
                                    it.files = filesVm;
                                    
                                    int count = 0;
                                    foreach (var file in filesEntity) {
                                       it.files[count].size = file.ArchivoData.Length;
                                       count++;
                                    }
                                    planRespVM.registros.Add(it);
                                }
                            }
                            planRespVM.count = planRespVM.registros.Count();
                            planVM.plan.Add(planRespVM);
                        }
                    }
                    return planVM;
                }

                else
                {
                    throw new ExceptionGeneral("No existe Plan de Accion !!");
                }
            }
        }
    }
}

    


