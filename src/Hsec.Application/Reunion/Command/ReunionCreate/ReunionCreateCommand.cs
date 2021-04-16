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
using Hsec.Application.Reunion.Models;
using Hsec.Application.Common.Models;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.Reunion.Command.ReunionCreate
{
    public class ReunionCreateCommand : IRequest<Unit>
    {

        public string Reunion { get; set; }

        public string Lugar { get; set; }

        public DateTime Fecha { get; set; }

        public string Hora { get; set; }

        public string CodPerFacilitador { get; set; }

        public string Acuerdos { get; set; }

        public string Comentarios { get; set; }

        public string Otros { get; set; }

        public ICollection<string> ReunionAsistentes { get; set; }

        public ICollection<string> ReunionAusentes { get; set; }

        public ICollection<string> ReunionJustificados { get; set; }

        public ICollection<TAgendaDto> ReunionAgendas { get; set; }

        public List<PlanVM> PlanAccion { get; set; }

        public class ReunionCreateCommandHandler : IRequestHandler<ReunionCreateCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ReunionCreateCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(ReunionCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    TReunionDto nuevo = new TReunionDto();
                    string maxCod = _context.TReunion.Max(r => r.CodReunion);
                    if (maxCod == null) maxCod = "REU00001";
                    else
                    {
                        int id = int.Parse(maxCod.Substring(3, 5)) + 1;
                        maxCod = "REU" + id.ToString("D5");
                    }

                    nuevo.CodReunion = maxCod;
                    nuevo.Reunion = request.Reunion;
                    nuevo.Lugar = request.Lugar;
                    nuevo.Fecha = request.Fecha;
                    nuevo.Hora = request.Hora;
                    nuevo.CodPerFacilitador = request.CodPerFacilitador;
                    nuevo.Acuerdos = request.Acuerdos;
                    nuevo.Comentarios = request.Comentarios;
                    nuevo.Otros = request.Otros;
                    nuevo.PlanAccion = request.PlanAccion;

                    foreach (string persona in request.ReunionAsistentes)
                    {
                        TAsistentesReunionDto reunionasistente = new TAsistentesReunionDto();
                        reunionasistente.CodPersona = persona;
                        nuevo.ReunionAsistentes.Add(reunionasistente);

                    }

                    foreach (var persona in request.ReunionAusentes)
                    {
                        TAusentesReunionDto reunionausente = new TAusentesReunionDto();
                        reunionausente.CodPersona = persona;
                        nuevo.ReunionAusentes.Add(reunionausente);

                    }

                    foreach (string persona in request.ReunionJustificados)
                    {
                        TJustificadosReunionDto reunionjustificado = new TJustificadosReunionDto();
                        reunionjustificado.CodPersona = persona;
                        nuevo.ReunionJustificados.Add(reunionjustificado);

                    }

                    //var corrmax = _context.TAgenda.DefaultIfEmpty().Max(t => (t.Correlativo));
                    var corrmax = _context.TAgenda.Count();
                    if(corrmax != 0)
                    {
                        corrmax = _context.TAgenda.DefaultIfEmpty().Max(t => (t.Correlativo));
                    }



                    //var corrmax = 0;
                    foreach (var agenda in request.ReunionAgendas)
                    {
                        TAgendaDto reunionagenda = new TAgendaDto();
                        reunionagenda.Correlativo = nextCorrelativo(corrmax);
                        reunionagenda.DesAgenda = agenda.DesAgenda;
                        nuevo.ReunionAgendas.Add(reunionagenda);
                        corrmax = nextCorrelativo(corrmax);
                    }

                    var reunion = _mapper.Map<TReunionDto, TReunion>(nuevo);
                    _context.TReunion.Add(reunion);

                    await _context.SaveChangesAsync(cancellationToken);

                    //await _planAccion.Create(nuevo.PlanAccion, maxCod);
                    nuevo.PlanAccion.ForEach(t => { t.docReferencia = maxCod; t.docSubReferencia = maxCod; });
                    var r1 = await _mediator.Send(new CreatePlanAccionCommand() { planes = nuevo.PlanAccion });


                    return Unit.Value;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            public int nextCorrelativo( int corrmax)
            {

                if (corrmax == null) corrmax = 1;
                else
                {
                    corrmax++;
                }
                return corrmax;
            }
        }
    }
}
