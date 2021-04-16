using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Incidentes.Models;
using Hsec.Domain.Entities.Incidentes;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.Files.Queries.GetFilesUpload;
using Hsec.Application.General.Aprobaciones.Queries.GetEstadoDoc;
using Hsec.Application.Files.Commands.DeleteFiles;
using Hsec.Application.Files.Commands.UpdateFiles;
using Hsec.Application.PlanAccion.Commands.DeletePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;
using Hsec.Application.Files.Commands.CreateFiles;

namespace Hsec.Application.Incidentes.Commands.UpdateIncidente
{

    public class UpdateIncidenteCommand : IRequest<int>
    {
        public IncidenteDto data { get; set; }
        public List<PlanVM> planAccion { get; set; }
        public IFormFileCollection newFiles { get; set; }
        public List<FilesUploadOneVM> updateFiles { get; set; }
        public string rol { get; set; }
        public class UpdateIncidenteCommandHandler : IRequestHandler<UpdateIncidenteCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public UpdateIncidenteCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            
            public async Task<int> Handle(UpdateIncidenteCommand request, CancellationToken cancellationToken)
            {
                // try{

                //var estado = await _general.GetEstadoAprobacion(request.data.CodIncidente, "TINC");
                var estado = await _mediator.Send(new GetEstadoDocQuery() { docReferencia = request.data.CodIncidente, codTabla = "TINC" });

                //assert(estado);

                IncidenteDto VM = request.data;
                string COD_INCIDENTE = VM.CodIncidente;

                TIncidente incidente = _context.TIncidente.Find(VM.CodIncidente);

                var rol = request.rol;
                if (incidente.EstadoIncidente.Equals("C") && (!(rol.Equals("1") || rol.Equals("4"))))
                {
                    throw new Exception("usuario no autorizado");
                }


                //datos generales
                incidente = _mapper.Map(VM.DatosGenerales, incidente);

                //ICAM
                incidente.Ticam = _context.TIcam.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                incidente.Ticam = _mapper.Map<IList<ICAMDto>, IList<TIcam>>(VM.ICAM);
                int indexIcan = 1;
                foreach (var item in incidente.Ticam) item.Correlativo = indexIcan++;

                //analisis Causa
                incidente.TincidenteAnalisisCausa = _context.TIncidenteAnalisisCausa.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                incidente.TincidenteAnalisisCausa = _mapper.Map<IList<AnalisisCausalidadDto>, IList<TIncidenteAnalisisCausa>>(VM.AnalisisCausalidad);

                //detalle
                incidente = _mapper.Map(VM.Detalle, incidente);

                incidente.TequipoInvestigacion = _context.TEquipoInvestigacion.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                incidente.TequipoInvestigacion = _mapper.Map<IList<EquipoInvestigacionDetalleDto>, IList<TEquipoInvestigacion>>(VM.Detalle.EquipoInvestigacion);
                int indexEI = 1;
                foreach (var item in incidente.TequipoInvestigacion) item.Correlativo = ++indexEI;
                incidente.TsecuenciaEvento = _context.TSecuenciaEvento.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                incidente.TsecuenciaEvento = _mapper.Map<IList<SecuenciaEventosDetalleDto>, IList<TSecuenciaEvento>>(VM.Detalle.SecuenciaEventos);
                int indexSE = 1;
                foreach (var item in incidente.TsecuenciaEvento) item.Correlativo = ++indexSE;
                incidente.TtestigoInvolucrado = _context.TTestigoInvolucrado.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                incidente.TtestigoInvolucrado = _mapper.Map<IList<TestigosInvolucradosDetalleDto>, IList<TTestigoInvolucrado>>(VM.Detalle.TestigosInvolucrados);
                int indexTI = 1;
                foreach (var item in incidente.TtestigoInvolucrado) item.Correlativo = ++indexTI;

                //estado cerrado o pentiente
                if (VM.EstadoIncidente != null && (VM.EstadoIncidente.Equals("P") || VM.EstadoIncidente.Equals("C")))
                {
                    incidente.EstadoIncidente = VM.EstadoIncidente;
                }
                else
                {
                    incidente.EstadoIncidente = "P";
                }

                //afectados
                var DetalleAfectado = _context.TDetalleAfectado.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).First();
                if (DetalleAfectado == null) 
                    DetalleAfectado = new TDetalleAfectado();
                DetalleAfectado = _mapper.Map<AfectadosDescipcionesAccionesDto, TDetalleAfectado>(VM.AfectadosDescipcionesAcciones,DetalleAfectado);
                _context.TDetalleAfectado.Update(DetalleAfectado);


                incidente.TafectadoComunidad = _context.TAfectadoComunidad.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                var deleteAfectadoComunidad = incidente.TafectadoComunidad.ToHashSet();

                incidente.TafectadoMedioAmbiente = _context.TAfectadoMedioAmbiente.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                var deleteAfectadoMedioAmbiente = incidente.TafectadoMedioAmbiente.ToHashSet();

                incidente.TafectadoPropiedad = _context.TAfectadoPropiedad.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                var deleteAfectadoPropiedad = incidente.TafectadoPropiedad.ToHashSet();

                incidente.TinvestigaAfectado = _context.TInvestigaAfectado.Where(t => t.CodIncidente.Equals(COD_INCIDENTE)).ToHashSet();
                var deleteinvetigaAfectado = incidente.TinvestigaAfectado.ToHashSet();

                var index = maximoNumero(incidente);

                foreach (var item in VM.AfectadosDescipcionesAcciones.Afectados)
                {
                    if (item.tipoAfectado == TipoAfectado.Comunidad)
                    {
                        var correlativo = item.Comunidad.Correlativo;
                        var data = _context.TAfectadoComunidad.Find(correlativo, COD_INCIDENTE);
                        if(data == null)
                        {
                            data = new TAfectadoComunidad();
                            data = _mapper.Map<ComunidadAfectadoDto, TAfectadoComunidad>(item.Comunidad);
                            data.Correlativo = Convert.ToInt32(++index);
                            incidente.TafectadoComunidad.Add(data);
                        }
                        else
                        {
                            data = _mapper.Map<ComunidadAfectadoDto, TAfectadoComunidad>(item.Comunidad,data);
                            _context.TAfectadoComunidad.Update(data);
                            deleteAfectadoComunidad.Remove(data);
                        }
                    }
                    else if (item.tipoAfectado == TipoAfectado.Medio_Ambiente)
                    {
                        var correlativo = item.MedioAmbiente.Correlativo;
                        var data = _context.TAfectadoMedioAmbiente.Find( correlativo, COD_INCIDENTE);
                        if(data == null)
                        {
                            data = new TAfectadoMedioAmbiente();
                            data = _mapper.Map<MedioAmbienteAfectadoDto, TAfectadoMedioAmbiente>(item.MedioAmbiente);
                            data.Correlativo = Convert.ToInt32(++index);
                            incidente.TafectadoMedioAmbiente.Add(data);
                        }
                        else
                        {
                            data = _mapper.Map<MedioAmbienteAfectadoDto, TAfectadoMedioAmbiente>(item.MedioAmbiente,data);
                            _context.TAfectadoMedioAmbiente.Update(data);
                            deleteAfectadoMedioAmbiente.Remove(data);
                        }
                    }
                    else if (item.tipoAfectado == TipoAfectado.Propiedad)
                    {
                        var correlativo = item.Propiedad.Correlativo;
                        var data = _context.TAfectadoPropiedad.Find( correlativo, COD_INCIDENTE);
                        if (data == null)
                        {
                            data = new TAfectadoPropiedad();
                            data = _mapper.Map<PropiedadAfectadoDto, TAfectadoPropiedad>(item.Propiedad);
                            data.Correlativo = Convert.ToInt32(++index);
                            incidente.TafectadoPropiedad.Add(data);
                        }
                        else
                        {
                            data = _mapper.Map<PropiedadAfectadoDto, TAfectadoPropiedad>(item.Propiedad,data);
                            _context.TAfectadoPropiedad.Update(data);
                            deleteAfectadoPropiedad.Remove(data);
                        }
                        
                    }
                    else if (item.tipoAfectado == TipoAfectado.Persona)
                    {
                        //var data = _context.TInvestigaAfectado.Find(new { CodIncidente = COD_INCIDENTE, Correlativo = item.Propiedad.Correlativo });
                        var correlativo = Convert.ToInt64(item.Persona.Correlativo);
                        var data = _context.TInvestigaAfectado.Find(COD_INCIDENTE, correlativo);
                        if (data == null)
                        {
                            data = new TInvestigaAfectado();
                            data = _mapper.Map<PersonaAfectadoDto, TInvestigaAfectado>(item.Persona);
                            data.Correlativo = Convert.ToInt32(++index);
                            incidente.TinvestigaAfectado.Add(data);
                            var diasPerdidiosAfectados = _mapper.Map<ICollection<MenAfectadoPersonaDto>, ICollection<TDiasPerdidosAfectado>>(item.Persona.Men);
                            int i = 1;
                            foreach (var itemDPA in diasPerdidiosAfectados)
                            {
                                itemDPA.CodPersona = data.DocAfectado;
                                itemDPA.Correlativo = i++;
                                itemDPA.CodIncidente = COD_INCIDENTE;
                            }
                            _context.TDiasPerdidosAfectado.AddRange(diasPerdidiosAfectados);
                        }
                        else
                        {
                            data = _mapper.Map<PersonaAfectadoDto, TInvestigaAfectado>(item.Persona,data);
                            var ListModeloAfectadoPersonasMen = item.Persona.Men;
                            _context.TInvestigaAfectado.Update(data);

                            IList<TDiasPerdidosAfectado> Men = _context.TDiasPerdidosAfectado.Where(t => t.CodIncidente.Equals(COD_INCIDENTE) && t.CodPersona.Equals(data.DocAfectado)).ToList();
                            var deleteMen = Men.ToList();
                            var maxIndexMen = Men.Select(t => t.Correlativo).DefaultIfEmpty(0).Max();

                            //IList<TDiasPerdidosAfectado> updateListDiasPerdidos = new List<TDiasPerdidosAfectado>();
                            foreach (var itemMen in ListModeloAfectadoPersonasMen)
                            {
                                var toInsertOrUpdate = Men.Where(t => t.Correlativo.ToString().Equals(itemMen.Correlativo)).FirstOrDefault();
                                
                                if(toInsertOrUpdate != null)
                                {
                                    toInsertOrUpdate = _mapper.Map<MenAfectadoPersonaDto, TDiasPerdidosAfectado>(itemMen, toInsertOrUpdate);
                                    _context.TDiasPerdidosAfectado.Update(toInsertOrUpdate);
                                    deleteMen.Remove(toInsertOrUpdate);
                                }
                                else
                                {
                                    toInsertOrUpdate = _mapper.Map<MenAfectadoPersonaDto, TDiasPerdidosAfectado>(itemMen);
                                    toInsertOrUpdate.CodIncidente = COD_INCIDENTE;
                                    toInsertOrUpdate.CodPersona = data.DocAfectado;
                                    toInsertOrUpdate.Correlativo = Convert.ToInt32(++maxIndexMen);
                                    _context.TDiasPerdidosAfectado.Add(toInsertOrUpdate);
                                }
                            }
                            _context.TDiasPerdidosAfectado.RemoveRange(deleteMen);
                            deleteinvetigaAfectado.Remove(data);
                        }
                        
                    }
                    else
                    {
                        throw new NotFoundException("", "");
                    }
                }

                _context.TAfectadoComunidad.RemoveRange(deleteAfectadoComunidad);
                _context.TAfectadoMedioAmbiente.RemoveRange(deleteAfectadoMedioAmbiente);
                _context.TAfectadoPropiedad.RemoveRange(deleteAfectadoPropiedad);
                _context.TInvestigaAfectado.RemoveRange(deleteinvetigaAfectado);

                _context.TIncidente.Update(incidente);
                await _context.SaveChangesAsync(cancellationToken);
                

                foreach (var item in request.updateFiles)
                {
                    if (item.Estado == false)
                    {
                        //var r1 = await _imagen.Delete(item.CorrelativoArchivos.ToString());
                        var r1 = await _mediator.Send(new DeleteFileCommand() { CorrelativoArchivos = item.CorrelativoArchivos });
                    }
                    else if (item.Estado == true && item.CorrelativoArchivos != 0)
                    {
                        //var r2 = await _imagen.Update(item);
                        var r2 = await _mediator.Send(new UpdateFileCommand() { CorrelativoArchivos = item.CorrelativoArchivos, Descripcion = item.Descripcion });
                    }
                }

                var updateList = new List<PlanVM>();
                var createList = new List<PlanVM>();
                foreach (var item in request.planAccion)
                {
                    if (item.estado == false && item.codAccion > 0)
                    {
                        //var r3 = await _planAccion.Delete(item.codAccion.ToString());
                        var r3 = await _mediator.Send(new DeletePlanAccionCommand() { CodAccion = item.codAccion });
                    }
                    else if (item.estado == true && item.codAccion > 0)
                    {
                        updateList.Add(item);
                    }
                    else if (item.codAccion < 0)
                    {
                        createList.Add(item);
                    }
                }
                //var r4 = await _planAccion.Create(createList, COD_INCIDENTE);
                createList.ForEach(t => { t.docReferencia = COD_INCIDENTE; t.docSubReferencia = COD_INCIDENTE; });
                var r4 = await _mediator.Send(new CreatePlanAccionCommand() { planes = createList });

                //var r5 = await _planAccion.Update(updateList);
                var r5 = await _mediator.Send(new UpdatePlanAccionCommand() { planes = updateList });

                //var r6 = await _imagen.Upload(request.newFiles, COD_INCIDENTE);
                var r6 = await _mediator.Send(new CreateListFilesCommand { File = request.newFiles, NroDocReferencia = COD_INCIDENTE, NroSubDocReferencia = COD_INCIDENTE, CodTablaRef = "TINC" });

                // }catch(Exception e){
                //     var ee = e;
                // }

                return 0;
            }
            
            public long maximoNumero(TIncidente incidente)
            {
                var maxComunidad = incidente.TafectadoComunidad.Select(t => t.Correlativo).DefaultIfEmpty(0).Max();
                var maxMediAmbiente = incidente.TafectadoMedioAmbiente.Select(t => t.Correlativo).DefaultIfEmpty(0).Max();
                var maxPropiedad = incidente.TafectadoPropiedad.Select(t => t.Correlativo).DefaultIfEmpty(0).Max();
                var maxAfectado = incidente.TinvestigaAfectado.Select(t => t.Correlativo).DefaultIfEmpty(0).Max();

                var maxCorrelativo = new[] { maxComunidad, maxMediAmbiente, maxPropiedad , maxAfectado };
                
                return maxCorrelativo.Max(); 
            }
        }
       
    }
}