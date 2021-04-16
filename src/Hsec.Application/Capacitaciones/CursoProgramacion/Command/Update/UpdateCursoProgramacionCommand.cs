using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Update.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Update.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Commands.CreatePersona;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Command.Update
{
    public class UpdateCursoProgramacionCommand : IRequest<Unit>
    {
        public UpdateCursoProgramacionDto DTO { get; set; }
        public class CreateCursoProgramacionCommandHandler : IRequestHandler<UpdateCursoProgramacionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            public CreateCursoProgramacionCommandHandler(IApplicationDbContext context, IMediator mediator)
            {
                this._context = context;
                this._mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateCursoProgramacionCommand request, CancellationToken cancellationToken)
            {
                var item = request.DTO;
                var curso = _context.TCurso.Include(i => i.Expositores).Include(i => i.Participantes).FirstOrDefault(i => i.CodCurso == item.codCurso && i.Estado);

                curso.CodTemaCapacita = item.codTemaCapacita;
                curso.CodTipoTema = item.codTipoTema;
                curso.CodAreaCapacita = item.codAreaCapacita;
                //curso.CodHha = item.codHha;
                curso.CodEmpCapacita = item.codEmpCapacita;
                curso.PuntajeTotal = item.puntajeTotal;
                curso.PorcAprobacion = item.porcAprobacion;
                curso.CodLugarCapacita = item.codLugarCapacita;
                curso.CodSala = item.codSala;
                curso.Capacidad = item.capacidad;
                curso.Vigencia = item.vigencia;
                curso.CodVigenciaCapacita = item.codVigenciaCapacita;
                curso.Enlace = item.enlace;
                curso.Online = item.online;
                curso.Duracion = item.duracion;
                if (item.fechaFin > item.fechaInicio) {
                    curso.FechaInicio = item.fechaInicio;
                    curso.FechaFin = item.fechaFin;
                }

                IList<TExpositor> c = new List<TExpositor>();
                //update expositores
                //if (item.expositores.Count() > 0)     {
                    var respBD = curso.Expositores.ToList();
                    var respJson = item.expositores.ToList();

                    var inter = respJson.Select(x => x.codPersona).Intersect(respBD.Select(x => x.CodPersona)).ToList(); //update
                    var right = respJson.Select(x => x.codPersona).Except(respBD.Select(x => x.CodPersona)).ToList(); // new
                    var left = respBD.Select(x => x.CodPersona).Except(respJson.Select(x => x.codPersona)).ToList();// delete

                    if (inter != null)
                    {
                        foreach (var CodPersona in inter)
                        {
                            var capBD = respBD.FirstOrDefault(i => i.CodPersona == CodPersona && i.CodCurso == curso.CodCurso && i.Estado);
                            if (capBD != null)
                            {
                                var capJson = respJson.FirstOrDefault(i => i.codPersona == CodPersona && i.codCurso == curso.CodCurso);
                                capBD.Tipo = capJson.tipo;
                                capBD.CodPersona = capJson.codPersona;
                                if (!capJson.tipo && String.IsNullOrEmpty(capJson.codPersona))
                                {
                                //var expositorExtCodPersona = await _expoExt.RequestCodPersonaExpositor(capJson.personaExterna);
                                var expositorExtCodPersona = await _mediator.Send(new CreatePersonaExpositorCommand() { Sexo = capJson.personaExterna.sexo.ToString(), NroDocumento = capJson.personaExterna.nroDocumento, Nombres = capJson.personaExterna.nombres, ApellidoPaterno = capJson.personaExterna.apellidoPaterno, ApellidoMaterno = capJson.personaExterna.apellidoMaterno, Ocupacion = capJson.personaExterna.ocupacion, Empresa = capJson.personaExterna.empresa });
                                capBD.CodPersona = expositorExtCodPersona;
                                }
                                _context.TExpositor.Update(capBD);
                            }
                        }
                    }

                    if (right != null)
                    {
                        foreach (var CodPersona in right)
                        {
                            var expoJson = respJson.FirstOrDefault(i => i.codPersona == CodPersona && i.codCurso == curso.CodCurso);
                            if (expoJson != null)
                            {
                                var expoEntity = new TExpositor();

                                expoEntity.CodCurso = expoJson.codCurso;
                                expoEntity.CodPersona = expoJson.codPersona;
                                expoEntity.Estado = true;
                                expoEntity.Tipo = expoJson.tipo;
                                expoEntity.Curso = curso;
                                _context.TExpositor.Add(expoEntity);
                            }
                        }
                    }

                    if (left != null)
                    {
                        foreach (var CodPersona in left)
                        {
                            var expoBD = respBD.FirstOrDefault(i => i.CodPersona == CodPersona && i.CodCurso == curso.CodCurso && i.Estado);
                            if (expoBD != null)
                            {
                                expoBD.Estado = false;
                                _context.TExpositor.Update(expoBD);
                            }
                        }
                    }
                //}
                //update participantes
               // if (item.participantes.Count() > 0)                {
                    var partiBD = curso.Participantes.ToList();
                    var partiJson = item.participantes.ToList();

                    var interp = partiJson.Select(x => x.codPersona).Intersect(partiBD.Select(x => x.CodPersona)).ToList();
                    var rightp = partiJson.Select(x => x.codPersona).Except(partiBD.Select(x => x.CodPersona)).ToList();
                    var leftp = partiBD.Select(x => x.CodPersona).Except(partiJson.Select(x => x.codPersona)).ToList();

                    if (inter != null)
                    {
                        foreach (var CodPersona in interp)
                        {
                            var partBD = partiBD.FirstOrDefault(i => i.CodPersona == CodPersona && i.CodCurso == curso.CodCurso && i.Estado);
                            if (partBD != null)
                            {
                                var partJson = partiJson.FirstOrDefault(i => i.codPersona == CodPersona && i.codCurso == curso.CodCurso);
                                partBD.Tipo = partJson.tipo;
                                partBD.Nota = partJson.nota;
                                partBD.CodPersona = partJson.codPersona;

                                _context.TParticipantes.Update(partBD);
                            }
                        }
                    }

                    if (right != null)
                    {
                        foreach (var CodPersona in rightp)
                        {
                            var partJson = partiJson.FirstOrDefault(i => i.codPersona == CodPersona && i.codCurso == curso.CodCurso);
                            if (partJson != null)
                            {
                                var partEntity = new TParticipantes();

                                partEntity.CodCurso = partJson.codCurso;
                                partEntity.CodPersona = partJson.codPersona;
                                partEntity.Estado = true;
                                partEntity.Tipo = partJson.tipo;
                                partEntity.Nota = partJson.nota;
                                partEntity.Curso = curso;
                                _context.TParticipantes.Add(partEntity);
                            }
                        }
                    }

                    if (left != null)
                    {
                        foreach (var CodPersona in leftp)
                        {
                            var partBD = partiBD.FirstOrDefault(i => i.CodPersona == CodPersona && i.CodCurso == curso.CodCurso && i.Estado);
                            if (partBD != null)
                            {
                                partBD.Estado = false;
                                _context.TParticipantes.Update(partBD);
                            }
                        }
                    }
               // }
                _context.TCurso.Update(curso);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}