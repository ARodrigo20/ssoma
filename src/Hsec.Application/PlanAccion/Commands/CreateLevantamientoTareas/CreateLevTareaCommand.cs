using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
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

namespace Hsec.Application.PlanAccion.Commands.CreateLevantamientoTareas
{
    public class CreateLevTareaCommand : IRequest<PlanCompVM>
    {
        public IFormCollection File { get; set; }

        public class CreateLevTareaCommandHandler : IRequestHandler<CreateLevTareaCommand, PlanCompVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFileLevTareaService IService;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public CreateLevTareaCommandHandler(IApplicationDbContext context, IFileLevTareaService IService, IMapper mapper, IMediator mediator)
            {
                this._context = context;
                this.IService = IService;
                this._mapper = mapper;
                this._mediator = mediator;
            }

            public async Task<PlanCompVM> Handle(CreateLevTareaCommand request, CancellationToken cancellationToken)
            {
                PlanCompVM planVM = new PlanCompVM();
                TFile file;
                var cabeceraDTO = IService.imgUpload(request.File);

                int codAccion = cabeceraDTO.codAccion;
                string codPersona = cabeceraDTO.codPersona;

                var validandoRespLevTarea = _context.TResponsable.Where(i => i.CodAccion == codAccion && i.CodPersona == codPersona && i.Estado);


                if (validandoRespLevTarea.Count() > 0)
                {
                    TLevantamientoPlan levan = new TLevantamientoPlan();
                    levan.CodAccion = codAccion;
                    levan.CodPersona = codPersona;
                    //lev.Correlativo = cabeceraDTO.correlativo;
                    levan.Descripcion = cabeceraDTO.descripcion;
                    levan.Fecha = cabeceraDTO.fecha;
                    levan.PorcentajeAvance = cabeceraDTO.porcentajeAvance;
                    levan.Rechazado = false;
                    //levan.Creado = DateTime.Now;
                    //levan.CreadoPor = "admin";
                    //levan.Modificado = DateTime.Now;
                    //levan.ModificadoPor = "admin";

                    int verificador = _context.TLevantamientoPlan.Count();
                    var ultCodCorrelativo = 1;
                    if (verificador != 0)
                    {
                        ultCodCorrelativo = _context.TLevantamientoPlan.Max(p => p.Correlativo) + 1;
                    }
                    IFormFileCollection files = request.File.Files;
                    foreach (var fil in files)
                    {
                        file = new TFile();
                        if (fil.Length < 0) { throw new GeneralFailureException("Imagen no valida"); }
                        file.ArchivoData = trans(fil);
                        file.TipoArchivo = fil.ContentType;
                        file.Estado = true;
                        file.Nombre = fil.FileName;
                        file.Descripcion = fil.Name;
                        //file.NroDocReferencia = ultCodCorrelativo.ToString();
                        ////file.NroSubDocReferencia = cabeceraDTO.nroSubDocReferencia;
                        file.NroDocReferencia = codAccion.ToString();
                        file.NroSubDocReferencia = ultCodCorrelativo.ToString();
                        file.CodTablaRef = "TACME";
                        _context.TFile.Add(file);
                    }
                    _context.TLevantamientoPlan.Add(levan);
                    await _context.SaveChangesAsync(cancellationToken);

                    var planAccionList = _context.TAccion.Include(i => i.RespPlanAccion).FirstOrDefault(i => i.CodAccion == codAccion && i.Estado);

                    //PlanRespVM plan = planAccionList.AsQueryable()
                    //.ProjectTo<PlanRespVM>(_mapper.ConfigurationProvider)
                    //.ToList().First();
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
                    var accion = _context.TAccion.FirstOrDefault(i => i.CodAccion == codAccion && i.Estado);
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
                    var levs = _context.TLevantamientoPlan.Where(i => i.CodAccion == codAccion && i.Estado);

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
                        levTareasVM.Rechazado = item.Rechazado;
                        if (nombPerson.Count != 0)
                        {
                            levTareasVM.nombres = nombPerson[0];
                        }
                        //result = _context.TFile.Where(i => i.NroDocReferencia == item.Correlativo.ToString()).ToList();
                        result = _context.TFile.Where(i => i.NroDocReferencia == item.CodAccion.ToString()  &&  i.NroSubDocReferencia == item.Correlativo.ToString()).ToList();

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
                else
                {
                    throw new GeneralFailureException("ERROR !! su levantamiento de tarea no esta ligado resposable !!");
                }
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
