using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.PlanAccion.Commands.CreateLevantamientoTareas;
using Hsec.Application.PlanAccion.Queries.GetPlanDeAccion;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;
using Hsec.Application.General.Personas.Queries.GetCode2Name;

namespace Hsec.Application.PlanAccion.Commands.UpdateLevantamientoTareas
{
    public class UpdateLevTareaCommand : IRequest<PlanCompVM>
    {
        public IFormCollection File { get; set; }
        public class UpdateLevTareaCommandHandler : IRequestHandler<UpdateLevTareaCommand, PlanCompVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFileLevTareaService IService;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public UpdateLevTareaCommandHandler(IApplicationDbContext context, IFileLevTareaService IService, IMapper mapper, IMediator mediator)
            {
                this._context = context;
                this.IService = IService;
                this._mapper = mapper;
                this._mediator = mediator;

            }

            public async Task<PlanCompVM> Handle(UpdateLevTareaCommand request, CancellationToken cancellationToken)
            {
                PlanCompVM planVM = new PlanCompVM();
                var cabeceraDTO = IService.imgUpload(request.File);

                var correlativo = cabeceraDTO.correlativo;
                var descripcion = cabeceraDTO.descripcion;
                var estadoNew = cabeceraDTO.estado;
                var porcentaje = cabeceraDTO.porcentajeAvance;
                var fecha = cabeceraDTO.fecha;

                var levTarea = _context.TLevantamientoPlan.FirstOrDefault(i => i.Correlativo == correlativo && i.Estado);

                levTarea.Descripcion = descripcion;
                levTarea.Estado = estadoNew;
                levTarea.PorcentajeAvance = porcentaje;
                levTarea.Fecha = fecha;


                if (!estadoNew)
                {
                    List<TFile> fil = _context.TFile.Where(I => I.NroDocReferencia == correlativo.ToString() && I.Estado).ToList();

                    foreach (var item in fil)
                    {
                        item.Estado = false;
                        _context.TFile.Update(item);
                    }
                }
                _context.TLevantamientoPlan.Update(levTarea);
                await _context.SaveChangesAsync(cancellationToken);

                // Se muestra el resultado como si fuera GET
                var planAccionList = _context.TAccion.Include(i => i.RespPlanAccion).FirstOrDefault(i => i.CodAccion == levTarea.CodAccion && i.Estado);

                PlanRespVM plan = new PlanRespVM();
                plan.codAccion = planAccionList.CodAccion;


                ResponsablesDto planRespDto;
                foreach (var it in planAccionList.RespPlanAccion)
                {
                    if (it.Estado)
                    {
                        var datos = _context.TLevantamientoPlan.Where(i => i.CodAccion == it.CodAccion && i.CodPersona == it.CodPersona && i.Estado);
                        planRespDto = new ResponsablesDto();
                        planRespDto.codAccion = it.CodAccion;
                        planRespDto.codPersona = it.CodPersona;
                        planRespDto.porcentajeMayor = 0;
                        planRespDto.estado = it.Estado;
                        if (datos.Count() > 0)
                        {
                            var max = datos.Max(i => i.PorcentajeAvance);
                            planRespDto.porcentajeMayor = max;
                        }
                        PersonaVM respVM = new PersonaVM();

                        if (!String.IsNullOrEmpty(it.CodPersona))
                        {
                            //respVM = await _persons.RequestNombApellidos(it.CodPersona);
                            respVM = await _mediator.Send(new GetPersonaQuery() { CodPersona = it.CodPersona });
                        }
                        if (respVM != null)
                        {
                            planRespDto.nombres = respVM.Nombres;
                            planRespDto.apellidoPaterno = respVM.ApellidoPaterno;
                            planRespDto.apellidoMaterno = respVM.ApellidoMaterno;
                        }
                        else
                        {
                            planRespDto.nombres = "";
                            planRespDto.apellidoPaterno = "";
                            planRespDto.apellidoMaterno = "";
                        }

                        plan.RespPlanAccion.Add(planRespDto);
                    }
                }

                var sumaDiv = (plan.RespPlanAccion.Sum(i => i.porcentajeMayor) / plan.RespPlanAccion.Count());

                var accion = _context.TAccion.FirstOrDefault(i => i.CodAccion == levTarea.CodAccion && i.Estado);
                //double sumatoria = 0;
                if (sumaDiv > 0 && sumaDiv < 100)
                {
                    accion.CodEstadoAccion = "03";
                }

                else if (sumaDiv >= 100)
                {
                    accion.CodEstadoAccion = "02";
                }

                else
                {
                    accion.CodEstadoAccion = "01";
                }
                plan.codEstadoAccion = accion.CodEstadoAccion;

                _context.TAccion.Update(accion);
                await _context.SaveChangesAsync(cancellationToken);

                LevTareasFilesVM levTareasVM;
                var levs = _context.TLevantamientoPlan.Where(i => i.CodAccion == levTarea.CodAccion && i.Estado);

                List<String> codPersonas;
                List<TFile> result;
                foreach (var item in levs)
                {
                    result = new List<TFile>();
                    codPersonas = new List<String>();
                    codPersonas.Add(item.CodPersona);
                    //List<String> nombPerson = await _persons.Request(codPersonas);
                    List<String> nombPerson = await _mediator.Send(new GetCode2NameQuery() { data = codPersonas });
                    levTareasVM = new LevTareasFilesVM();
                    levTareasVM.codAccion = item.CodAccion;
                    levTareasVM.codPersona = item.CodPersona;
                    levTareasVM.correlativo = item.Correlativo;
                    levTareasVM.descripcion = item.Descripcion;
                    levTareasVM.estado = item.Estado;
                    levTareasVM.fecha = item.Fecha;
                    levTareasVM.porcentajeAvance = item.PorcentajeAvance;
                    levTareasVM.nombres = "";
                    if (nombPerson.Count != 0)
                    {
                        levTareasVM.nombres = nombPerson[0];
                    }
                    result = _context.TFile.Where(i => i.NroDocReferencia == item.Correlativo.ToString()).ToList();

                    FilesDto filesDto;

                    foreach (var it in result)
                    {
                        if (item.Estado == true)
                        {
                            filesDto = new FilesDto();

                            filesDto.size = it.ArchivoData.Length;
                            filesDto.correlativoArchivos = it.CorrelativoArchivos;
                            filesDto.descripcion = it.Descripcion;
                            filesDto.grupoPertenece = it.GrupoPertenece;
                            filesDto.nombre = it.Nombre;
                            filesDto.nroDocReferencia = it.NroDocReferencia;
                            filesDto.nroSubDocReferencia = it.NroSubDocReferencia;
                            filesDto.tipoArchivo = it.TipoArchivo;
                            filesDto.estado = it.Estado;
                            levTareasVM.files.Add(filesDto);


                        }
                    }
                    plan.registros.Add(levTareasVM);
                }
                plan.count = plan.registros.Count;
                planVM.plan.Add(plan);
                return planVM;
            }
            private byte[] trans(IFormFile file)
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
                return fileBytes;
            }
        }
    }
}
