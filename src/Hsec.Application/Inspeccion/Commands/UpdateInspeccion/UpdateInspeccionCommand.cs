using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Application.Inspeccion.Models;
using Hsec.Domain.Entities.Inspecciones;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Hsec.Application.Files.Queries.GetFilesUpload;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;
using Hsec.Application.Files.Commands.CreateOneFile;
using Hsec.Application.Files.Commands.UpdateFiles;
using Hsec.Application.Files.Commands.DeleteFiles;

namespace Hsec.Application.Inspeccion.Commands.UpdateInspeccion
{
    public class UpdateInspeccionCommand : IRequest<Unit>
    {
        public InspeccionDto Inspeccion { get; set; }
        public List<EquipoDto> Equipo { get; set; }
        public List<AtendidosDto> Atendidos { get; set; }
        public List<DetalleInspeccionDto> Observaciones { get; set; }
        public List<InspeccionAnalisisCausaDto> AnalisisCausa { get; set; }
        public List<FilesUploadOneVM> UpdateFiles { get; set; }
        public IFormFileCollection Files { get; set; }

        public class UpdateInspeccionCommandHandler : IRequestHandler<UpdateInspeccionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public UpdateInspeccionCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateInspeccionCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Inspeccion == null) throw new Exception("falta Inspeccion");
                    string codigo = request.Inspeccion.CodInspeccion;

                    var inspeccion = _context.TInspeccion.Where(t => t.Estado == true && t.CodInspeccion.Equals(codigo)).FirstOrDefault();
                    
                    if (inspeccion == null) throw new Exception("error no existe el dato " + codigo);

                    inspeccion = _mapper.Map<InspeccionDto, TInspeccion>(request.Inspeccion, inspeccion);
                    _context.TInspeccion.Update(inspeccion);
                    var num1 = await _context.SaveChangesAsync(cancellationToken);

                    if (request.Observaciones != null)
                    {
                        foreach (var _detalle in request.Observaciones)
                        {
                            var correlativo = _detalle.Correlativo;

                            if(correlativo < 0)
                            {
                                TDetalleInspeccion newDetalle = new TDetalleInspeccion();
                                newDetalle.CodInspeccion = codigo;
                                newDetalle.CodTabla = _detalle.CodTabla;
                                newDetalle.Lugar = _detalle.Lugar;
                                newDetalle.CodUbicacion = _detalle.CodUbicacion;
                                newDetalle.CodAspectoObs = _detalle.CodAspectoObs;
                                newDetalle.CodActividadRel = _detalle.CodActividadRel;
                                newDetalle.Observacion = _detalle.Observacion;
                                newDetalle.CodNivelRiesgo = _detalle.CodNivelRiesgo;
                                _context.TDetalleInspeccion.Add(newDetalle);

                                if (_detalle.PlanesAccion != null && _detalle.PlanesAccion.Count > 0)
                                {
                                    //var num2 = await _planAccion.Create(_detalle.PlanesAccion, codigo, newDetalle.Correlativo.ToString());
                                    _detalle.PlanesAccion.ForEach(t => { t.docReferencia = codigo; t.docSubReferencia = newDetalle.Correlativo.ToString(); });
                                    var num2 = await _mediator.Send(new CreatePlanAccionCommand() { planes = _detalle.PlanesAccion });
                                }
                                if (request.Files != null)
                                {
                                    foreach (var _file in request.Files)
                                    {
                                        var decodeFileName = Base64Decode(_file.Name);
                                        var obj = JsonConvert.DeserializeObject<CreateFileAtributesVCCDto>(decodeFileName);

                                        if (obj.CodDetalle == _detalle.Correlativo.ToString())
                                        {
                                            //var num3 = await _imagenes.UploadOneFile(_file, codigo, newDetalle.Correlativo.ToString());
                                            var num3 = await _mediator.Send(new CreateOneFileCommand { File = _file, NroDocReferencia = codigo, NroSubDocReferencia = newDetalle.Correlativo.ToString(), CodTablaRef = "TINSP" });
                                        }

                                    }
                                }
                            }
                            else
                            {
                                List<PlanVM> planesM = new List<PlanVM>();
                                List<PlanVM> planAccionVmList = new List<PlanVM>();
                                if (_detalle.PlanesAccion != null && _detalle.PlanesAccion.Count > 0)
                                {
                                    foreach (PlanVM planAccionVm in _detalle.PlanesAccion)
                                    {
                                        if (planAccionVm.codAccion < 0)
                                            planAccionVmList.Add(planAccionVm);
                                        if (planAccionVm.codAccion > 0)
                                            planesM.Add(planAccionVm);
                                    }
                                }
                                TDetalleInspeccion entity = _context.TDetalleInspeccion.Where(t => t.Estado == true && t.Correlativo.Equals(_detalle.Correlativo)).FirstOrDefault();
                                if (inspeccion == null) throw new Exception("error no existe el dato " + codigo);
                                entity.CodTabla = _detalle.CodTabla;
                                entity.Lugar = _detalle.Lugar;
                                entity.CodUbicacion = _detalle.CodUbicacion;
                                entity.CodAspectoObs = _detalle.CodAspectoObs;
                                entity.CodActividadRel = _detalle.CodActividadRel;
                                entity.Observacion = _detalle.Observacion;
                                entity.CodNivelRiesgo = _detalle.CodNivelRiesgo;
                                entity.Estado = _detalle.Estado;
                                _context.TDetalleInspeccion.Update(entity);

                                //var num3 = await _planAccion.Create(planAccionVmList, codigo, correlativo.ToString());
                                planAccionVmList.ForEach(t => { t.docReferencia = codigo; t.docSubReferencia = correlativo.ToString(); });
                                var num3 = await _mediator.Send(new CreatePlanAccionCommand() { planes = planAccionVmList });

                                //var num4 = await _planAccion.Update(planesM);
                                var num4 = await _mediator.Send(new UpdatePlanAccionCommand() { planes = planesM });
                            }

                            if (request.Files != null)
                            {

                                foreach (var _file in request.Files)
                                {
                                    var decodeFileName = Base64Decode(_file.Name);
                                    var obj = JsonConvert.DeserializeObject<CreateFileAtributesVCCDto>(decodeFileName);

                                    if (obj.CodDetalle == _detalle.Correlativo.ToString())
                                    {
                                        //var num3 = await _imagenes.UploadOneFile(_file, codigo, correlativo.ToString());
                                        var num5 = await _mediator.Send(new CreateOneFileCommand { File = _file, NroDocReferencia = codigo, NroSubDocReferencia = correlativo.ToString(), CodTablaRef = "TINSP" });
                                    }

                                }
                            }
                        }

                    }
                    if (request.Equipo != null)
                    {
                        var old_personas = _context.TEquipoInspeccion.Where(t => t.CodInspeccion == codigo).ToList();
                        //var personas = _context.TEquipoInspeccion.Where(t => t.CodInspeccion == codigo)
                        foreach (var persona in old_personas)
                        {
                            _context.TEquipoInspeccion.Remove(persona);
                        }
                        IList<TEquipoInspeccion> lista = _mapper.Map<IList<EquipoDto>, IList<TEquipoInspeccion>>(request.Equipo);
                        foreach (var persona in lista)
                        {
                            persona.CodInspeccion = codigo;
                        }
                        _context.TEquipoInspeccion.AddRange(lista);
                    }
                    if (request.Atendidos != null)
                    {
                        var old_personas = _context.TPersonaAtendida.Where(t => t.CodInspeccion == codigo).ToList();
                        foreach (var persona in old_personas)
                        {
                            _context.TPersonaAtendida.Remove(persona);
                        }
                        IList<TPersonaAtendida> lista = _mapper.Map<IList<AtendidosDto>, IList<TPersonaAtendida>>(request.Atendidos);
                        foreach (var persona in lista)
                        {
                            persona.CodInspeccion = codigo;
                        }
                        _context.TPersonaAtendida.AddRange(lista);
                    }
                    if (request.AnalisisCausa != null)
                    {
                        var old_analisis = _context.TInspeccionAnalisisCausa.Where(t => t.CodInspeccion == codigo).ToList();
                        foreach (var analisis in old_analisis)
                        {
                            _context.TInspeccionAnalisisCausa.Remove(analisis);
                        }
                        IList<TInspeccionAnalisisCausa> lista = _mapper.Map<IList<InspeccionAnalisisCausaDto>, IList<TInspeccionAnalisisCausa>>(request.AnalisisCausa);
                        foreach (var analisis in lista)
                        {
                            analisis.CodInspeccion = codigo;
                        }
                        _context.TInspeccionAnalisisCausa.AddRange(lista);
                    }
                    if (request.UpdateFiles != null)
                    {
                        foreach (var updateFile in request.UpdateFiles)
                        {
                            if (updateFile.Estado)
                            {
                                //var num2 = await _imagenes.Update(updateFile);
                                var num2 = await _mediator.Send(new UpdateFileCommand() { CorrelativoArchivos = updateFile.CorrelativoArchivos, Descripcion = updateFile.Descripcion });
                            }
                            else
                            {
                                //var num3 = await _imagenes.Delete(updateFile.correlativoArchivos.ToString());
                                var num3 = await _mediator.Send(new DeleteFileCommand() { CorrelativoArchivos = updateFile.CorrelativoArchivos });
                            }
                        }
                    }

                    var num6 = await _context.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public static string Base64Decode(string base64EncodedData)
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
        }
    }
}