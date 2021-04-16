using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.DeletePlanDeAccion;
using Hsec.Application.Files.Queries.GetFilesUpload;
using Hsec.Application.Files.Commands.UpdateFiles;
using Hsec.Application.Files.Commands.DeleteFiles;
using Hsec.Application.Files.Commands.CreateFiles;

using Microsoft.AspNetCore.Http;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion {
    public class UpdateObservacionCommand : IRequest<Unit> {
        public ObservacionDto data { get; set; }

        public class UpdateObservacionCommandHandler : IRequestHandler<UpdateObservacionCommand, Unit> {
            protected readonly IApplicationDbContext _context;
            protected readonly IMapper _mapper;
            protected readonly IMediator _mediator;

            private readonly IList<Tuple<string, Strategia>> _tipos;
            private Strategia _strategia;
            public UpdateObservacionCommandHandler (IApplicationDbContext context, IMapper mapper, IMediator mediator) {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
                _tipos =
                   new List<Tuple<string, Strategia>> () {
                       new Tuple<string, Strategia> ("1", new ComportamientoSubHandler (_context, _mapper)),
                       new Tuple<string, Strategia> ("2", new CondicionSubHandler (_context, _mapper)),
                       new Tuple<string, Strategia> ("3", new TareaSubHandler (_context, _mapper)),
                       new Tuple<string, Strategia> ("4.1", new IteraccionSeguridadSubHandler (_context, _mapper)),
                       new Tuple<string, Strategia> ("4.2", new VerificacionControlCriticoSubHandle (_context, _mapper)),
                       new Tuple<string, Strategia> ("4.3", new Covid19SubHandle (_context, _mapper))
                   };
            }

            public async Task<Unit> Handle (UpdateObservacionCommand request, CancellationToken cancellationToken) {
                try {
                    ObservacionDto obsDto = request.data;
                    string CodObservacion = obsDto.CodObservacion;

                    TObservacion obs = _context.TObservaciones.First (o => o.CodObservacion.Equals (CodObservacion));

                    //int oldTipo = obs.CodTipoObservacion;
                    string oldTipo = (obs.CodTipoObservacion == 4) ? obs.CodTipoObservacion + "." + obs.CodSubTipoObs : obs.CodTipoObservacion.ToString();

                    if (obsDto.CodTipoObservacion.Length > 1)
                    {
                        obsDto.CodSubTipoObs = obsDto.CodTipoObservacion.Substring(2, 1);
                        obsDto.CodTipoObservacion = obsDto.CodTipoObservacion.Substring(0, 1);
                    }

                    obs = _mapper.Map<ObservacionDto, TObservacion> (obsDto, obs);

                    //int newTipo = obs.CodTipoObservacion;
                    string newTipo = (obs.CodTipoObservacion == 4) ? obs.CodTipoObservacion + "." + obs.CodSubTipoObs : obs.CodTipoObservacion.ToString();

                    _strategia = _tipos.First (t => t.Item1 == oldTipo).Item2; // seleccion de subtipo
                    //_strategia.DeleteSubtipo (obs.CodObservacion);
                    _strategia.DeleteDetalleSubtipo (obs.CodObservacion);

                    _strategia = _tipos.First (t => t.Item1 == newTipo).Item2; // seleccion de subtipo
                    _strategia.UpsertSubTipo (obsDto);
                    _strategia.CreateDetalleSubTipo (obsDto);




                    _context.TObservaciones.Update (obs);
                    await _context.SaveChangesAsync (cancellationToken);

                    foreach (var item in obsDto.FilesSTR) {
                        if (item.Estado == false) {
                            //var r1 = await _imagen.Delete (item.CorrelativoArchivos.ToString ());
                            var r1 = await _mediator.Send(new DeleteFileCommand() { CorrelativoArchivos = item.CorrelativoArchivos });
                        } else if (item.Estado == true && item.CorrelativoArchivos != 0) {
                            //var r2 = await _imagen.Update (item);
                            var r2 = await _mediator.Send(new UpdateFileCommand() { CorrelativoArchivos = item.CorrelativoArchivos, Descripcion = item.Descripcion });
                        }
                    }

                    var updateList = new List<PlanVM> ();
                    var createList = new List<PlanVM> ();
                    foreach (var item in obsDto.PlanAccion) {
                        if (item.estado == false && item.codAccion > 0) {
                            //var r3 = await _planAccion.Delete (item.codAccion.ToString ());
                            var r3 = await _mediator.Send(new DeletePlanAccionCommand() { CodAccion = item.codAccion });
                        } else if (item.estado == true && item.codAccion > 0) {
                            updateList.Add (item);
                        } else if (item.codAccion < 0) {
                            createList.Add (item);
                        }
                    }
                    //var r4 = await _planAccion.Create (createList, CodObservacion);
                    createList.ForEach(t => { t.docReferencia = CodObservacion; t.docSubReferencia = CodObservacion; });
                    var r4 = await _mediator.Send(new CreatePlanAccionCommand() { planes = createList });

                    //var r5 = await _planAccion.Update (updateList);
                    var r5 = await _mediator.Send(new UpdatePlanAccionCommand() { planes = updateList });

                    //var r6 = await _imagen.Upload (obsDto.files, CodObservacion);
                    var r6 = await _mediator.Send(new CreateListFilesCommand { File = obsDto.files, NroDocReferencia = CodObservacion, NroSubDocReferencia = CodObservacion, CodTablaRef = "TOBS" });

                    return Unit.Value;
                } catch (Exception e) {
                    var ee = e;
                    throw e;
                }
            }

        }

    }
}