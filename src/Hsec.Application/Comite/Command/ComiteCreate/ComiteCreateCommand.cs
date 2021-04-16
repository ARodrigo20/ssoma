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
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.Files.Commands.CreateFiles;

namespace Hsec.Application.Comite.Command.ComiteCreate
{
    public class ComiteCreateCommand : IRequest<Unit>
    {

        public CreateComiteDto data { get; set; }

        public IFormFileCollection Files { get; set; }

        public class ComiteCreateCommandHandler : IRequestHandler<ComiteCreateCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ComiteCreateCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(ComiteCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.data == null) throw new Exception("falta Comite");
                    ComiteDto nuevo = new ComiteDto();

                    string maxCod = _context.TComite.Max(r => r.CodComite);
                    if (maxCod == null) maxCod = "COM0000000001";
                    else
                    {
                        int id = int.Parse(maxCod.Substring(3, 10)) + 1;
                        maxCod = "COM" + id.ToString("D10");
                    }

                    nuevo.CodComite = maxCod;
                    nuevo.Fecha = request.data.Fecha;
                    nuevo.Hora = request.data.Hora;
                    nuevo.Fecha = request.data.Fecha;
                    nuevo.CodTipoComite = request.data.CodTipoComite;
                    nuevo.CodCategoria = request.data.CodCategoria;
                    nuevo.CodPosicionGer = request.data.CodPosicionGer;
                    nuevo.CodPosicionSup = request.data.CodPosicionSup;
                    nuevo.Lugar = request.data.Lugar;
                    nuevo.DetalleApertura = request.data.DetalleApertura;
                    nuevo.CodSecretario = request.data.CodSecretario;
                    nuevo.ResumenSalud = request.data.ResumenSalud;
                    nuevo.ResumenSeguridad = request.data.ResumenSeguridad;
                    nuevo.ResumenMedioAmbiente = request.data.ResumenMedioAmbiente;
                    nuevo.ResumenComunidad = request.data.ResumenComunidad;
                    nuevo.FechaCierre = request.data.FechaCierre;
                    nuevo.HoraCierre = request.data.HoraCierre;
                    nuevo.PlanAccion = request.data.PlanAccion;

                    foreach (string persona in request.data.ListaParticipantes)
                    {
                        TListaParticipantesComiteDto participante = new TListaParticipantesComiteDto();
                        participante.CodPersona = persona;
                        nuevo.ListaParticipantes.Add(participante);

                    }

                    var comite = _mapper.Map<ComiteDto, TComite>(nuevo);
                    _context.TComite.Add(comite);

                    await _context.SaveChangesAsync(cancellationToken);

                    //var r2 = await _imagen.Upload(request.Files, maxCod, "TCOM");
                    var r2 = await _mediator.Send(new CreateListFilesCommand { File = request.Files, NroDocReferencia = maxCod, NroSubDocReferencia = maxCod, CodTablaRef = "TCOM" });


                    //var r3 = await _planAccion.Create(nuevo.PlanAccion, maxCod);
                    nuevo.PlanAccion.ForEach(t => { t.docReferencia = maxCod; t.docSubReferencia = maxCod; });
                    var r3 = await _mediator.Send(new CreatePlanAccionCommand() { planes = nuevo.PlanAccion });


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
