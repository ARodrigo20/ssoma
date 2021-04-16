using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Hsec.Application.Common.Exceptions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Domain.Entities.Otros;
using Hsec.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Hsec.Application.Comite.Models;
using Microsoft.EntityFrameworkCore;
using Hsec.Application.Comite.Command.ComiteCreate;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.Files.Queries.GetFilesUpload;
using Hsec.Application.Files.Commands.UpdateFiles;
using Hsec.Application.Files.Commands.DeleteFiles;
using Hsec.Application.PlanAccion.Commands.DeletePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;
using Hsec.Application.Files.Commands.CreateFiles;

namespace Hsec.Application.Comite.Command.ComiteUpdate
{
    public class ComiteUpdateCommand : IRequest<Unit>
    {

        public CreateComiteDto data { get; set; }

        public IFormFileCollection Files { get; set; }

        public List<FilesUploadOneVM> UpdateFiles { get; set; }

        public class ComiteUpdateCommandHandler : IRequestHandler<ComiteUpdateCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ComiteUpdateCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(ComiteUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.data == null) throw new Exception("falta Comite");

                    var comite = _context.TComite.Include(c => c.ListaParticipantes).First(t => t.CodComite.Equals(request.data.CodComite));

                    comite.Fecha = request.data.Fecha;
                    comite.Hora = request.data.Hora;
                    comite.Fecha = request.data.Fecha;
                    comite.CodTipoComite = request.data.CodTipoComite;
                    comite.CodCategoria = request.data.CodCategoria;
                    comite.CodPosicionGer = request.data.CodPosicionGer;
                    comite.CodPosicionSup = request.data.CodPosicionSup;
                    comite.Lugar = request.data.Lugar;
                    comite.DetalleApertura = request.data.DetalleApertura;
                    comite.CodSecretario = request.data.CodSecretario;
                    comite.ResumenSalud = request.data.ResumenSalud;
                    comite.ResumenSeguridad = request.data.ResumenSeguridad;
                    comite.ResumenMedioAmbiente = request.data.ResumenMedioAmbiente;
                    comite.ResumenComunidad = request.data.ResumenComunidad;
                    comite.FechaCierre = request.data.FechaCierre;
                    comite.HoraCierre = request.data.HoraCierre;

                    comite.ListaParticipantes = comite.ListaParticipantes.Where(l => l.Estado == true).ToList();

                    var interParticipantes = comite.ListaParticipantes.Select(x => x.CodPersona).Intersect(request.data.ListaParticipantes).ToList(); //update
                    var leftParticipantes = request.data.ListaParticipantes.Except(comite.ListaParticipantes.Select(x => x.CodPersona)).ToList(); //new
                    var rightParticipantes = comite.ListaParticipantes.Select(x => x.CodPersona).Except(request.data.ListaParticipantes).ToList(); //delete

                    foreach (var CodPersona in interParticipantes)
                    {
                        var pm = comite.ListaParticipantes.First(t => t.CodPersona.Equals(CodPersona));
                        pm.Estado = true;
                    }

                    foreach (var CodPersona in leftParticipantes)
                    {
                        TListaParticipantesComiteDto participante = new TListaParticipantesComiteDto();
                        participante.CodPersona = CodPersona;
                        var _participante = _mapper.Map<TListaParticipantesComiteDto, TListaParticipantesComite>(participante);
                        comite.ListaParticipantes.Add(_participante);
                    }

                    foreach (var CodPersona in rightParticipantes)
                    {
                        var pm = comite.ListaParticipantes.First(t => t.CodPersona.Equals(CodPersona));
                        pm.Estado = false;
                    }

                    _context.TComite.Update(comite);

                    await _context.SaveChangesAsync(cancellationToken);

                    if (request.UpdateFiles != null)
                    {
                        foreach (var updateFile in request.UpdateFiles)
                        {
                            if (updateFile.Estado)
                            {
                                //var num2 = await _imagen.Update(updateFile);
                                var num2 = await _mediator.Send(new UpdateFileCommand() { CorrelativoArchivos = updateFile.CorrelativoArchivos, Descripcion = updateFile.Descripcion });
                            }
                            else
                            {
                                //var num3 = await _imagen.Delete(updateFile.correlativoArchivos.ToString());
                                var num3 = await _mediator.Send(new DeleteFileCommand() { CorrelativoArchivos = updateFile.CorrelativoArchivos });
                            }
                        }
                    }

                    var updateList = new List<PlanVM>();
                    var createList = new List<PlanVM>();
                    if(request.data.PlanAccion != null && request.data.PlanAccion.Count()>0)
                    {
                        foreach (var item in request.data.PlanAccion)
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
                    }

                    //var r4 = await _planAccion.Create(createList, request.data.CodComite);
                    createList.ForEach(t => { t.docReferencia = request.data.CodComite; t.docSubReferencia = request.data.CodComite; });
                    var r4 = await _mediator.Send(new CreatePlanAccionCommand() { planes = createList });

                    //var r5 = await _planAccion.Update(updateList);
                    var r5 = await _mediator.Send(new UpdatePlanAccionCommand() { planes = updateList });

                    //var r6 = await _imagen.Upload(request.Files, request.data.CodComite, "TCOM");
                    var r6 = await _mediator.Send(new CreateListFilesCommand { File = request.Files, NroDocReferencia = request.data.CodComite, NroSubDocReferencia = request.data.CodComite, CodTablaRef = "TCOM" });

                    return Unit.Value;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
