using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Models;
using Hsec.Application.Reunion.Models;
using Hsec.Domain.Entities.Otros;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.DeletePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;

namespace Hsec.Application.Reunion.Command.ReunionUpdate
{
    public class ReunionUpdateCommand : IRequest<Unit>
    {
        public string CodReunion { get; set; }

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


        public class ReunionUpdateCommandHandler : IRequestHandler<ReunionUpdateCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ReunionUpdateCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(ReunionUpdateCommand request, CancellationToken cancellationToken)
            {
                var reunion = _context.TReunion.Include(p => p.ReunionAsistentes).Include(c => c.ReunionAusentes).Include(r => r.ReunionJustificados).Include(t => t.ReunionAgendas).First(t => t.CodReunion.Equals(request.CodReunion));

                reunion.Reunion = request.Reunion;
                reunion.Lugar = request.Lugar;
                reunion.Fecha = request.Fecha;
                reunion.Hora = request.Hora;
                reunion.CodPerFacilitador = request.CodPerFacilitador;
                reunion.Acuerdos = request.Acuerdos;
                reunion.Comentarios = request.Comentarios;
                reunion.Otros = request.Otros;

                //ASISTENTES
                reunion.ReunionAsistentes = reunion.ReunionAsistentes.Where(a => a.Estado == true).ToList();

                var interAsistentes = reunion.ReunionAsistentes.Select(x => x.CodPersona).Intersect(request.ReunionAsistentes).ToList(); //update
                var leftAsistentes = request.ReunionAsistentes.Except(reunion.ReunionAsistentes.Select(x => x.CodPersona)).ToList(); //new
                var rightAsistentes = reunion.ReunionAsistentes.Select(x => x.CodPersona).Except(request.ReunionAsistentes).ToList(); //delete

                foreach (var CodPersona in interAsistentes)
                {
                    var pm = reunion.ReunionAsistentes.First(t => t.CodPersona.Equals(CodPersona));
                    pm.Estado = true;
                }

                foreach (var CodPersona in leftAsistentes)
                {
                    TAsistentesReunionDto reunionasistente = new TAsistentesReunionDto();
                    reunionasistente.CodPersona = CodPersona;
                    var _reunionasistente = _mapper.Map<TAsistentesReunionDto, TAsistentesReunion>(reunionasistente);
                    reunion.ReunionAsistentes.Add(_reunionasistente);
                }

                foreach (var CodPersona in rightAsistentes)
                {
                    var pm = reunion.ReunionAsistentes.First(t => t.CodPersona.Equals(CodPersona));
                    pm.Estado = false;
                }


                //AUSENTES
                reunion.ReunionAusentes = reunion.ReunionAusentes.Where(a => a.Estado == true).ToList();

                var interAusentes = reunion.ReunionAusentes.Select(x => x.CodPersona).Intersect(request.ReunionAusentes).ToList(); //update
                var leftAusentes = request.ReunionAusentes.Except(reunion.ReunionAusentes.Select(x => x.CodPersona)).ToList(); //new
                var rightAusentes = reunion.ReunionAusentes.Select(x => x.CodPersona).Except(request.ReunionAusentes).ToList(); //delete

                foreach (var CodPersona in interAusentes)
                {
                    var cm = reunion.ReunionAusentes.First(t => t.CodPersona.Equals(CodPersona));
                    cm.Estado = true;
                }

                foreach (var CodPersona in leftAusentes)
                {
                    TAusentesReunionDto reunionausente = new TAusentesReunionDto();
                    reunionausente.CodPersona = CodPersona;
                    var _reunionasistente = _mapper.Map<TAusentesReunionDto, TAusentesReunion>(reunionausente);
                    reunion.ReunionAusentes.Add(_reunionasistente);
                }

                foreach (var CodPersona in rightAusentes)
                {
                    var cm = reunion.ReunionAusentes.First(t => t.CodPersona.Equals(CodPersona));
                    cm.Estado = false;
                }

                //JUSTIFICADOS
                reunion.ReunionJustificados = reunion.ReunionJustificados.Where(j => j.Estado == true).ToList();

                var interJustificados = reunion.ReunionJustificados.Select(x => x.CodPersona).Intersect(request.ReunionJustificados).ToList(); //update
                var leftJustificados = request.ReunionJustificados.Except(reunion.ReunionJustificados.Select(x => x.CodPersona)).ToList(); //new
                var rightJustificados = reunion.ReunionJustificados.Select(x => x.CodPersona).Except(request.ReunionJustificados).ToList(); //delete

                foreach (var CodPersona in interJustificados)
                {
                    var rm = reunion.ReunionJustificados.First(t => t.CodPersona.Equals(CodPersona));
                    rm.Estado = true;
                }

                foreach (var CodPersona in leftJustificados)
                {
                    TJustificadosReunionDto reunionjustificado = new TJustificadosReunionDto();
                    reunionjustificado.CodPersona = CodPersona;
                    var _reunionjustificado = _mapper.Map<TJustificadosReunionDto, TJustificadosReunion>(reunionjustificado);
                    reunion.ReunionJustificados.Add(_reunionjustificado);
                }

                foreach (var CodPersona in rightJustificados)
                {
                    var rm = reunion.ReunionJustificados.First(t => t.CodPersona.Equals(CodPersona));
                    rm.Estado = false;
                }

                //AGENDAS
                reunion.ReunionAgendas = reunion.ReunionAgendas.Where(a => a.Estado == true).ToList();

                var rightAgendas = reunion.ReunionAgendas.Select(x => x.Correlativo).Except(request.ReunionAgendas.Select(c => c.Correlativo)).ToList(); //delete

                foreach (var Correlativo in rightAgendas)
                {
                    var am = reunion.ReunionAgendas.First(t => t.Correlativo.Equals(Correlativo));
                    am.DesAgenda = "";
                    am.Estado = false;
                }

                var corrmax = _context.TAgenda.Max(t => t.Correlativo);
                foreach (var agenda in request.ReunionAgendas)
                {
                    if (agenda.Correlativo == 0)
                    {
                        TAgendaDto reunionagenda = new TAgendaDto();
                        reunionagenda.Correlativo = nextCorrelativo(corrmax);
                        reunionagenda.DesAgenda = agenda.DesAgenda;
                        var _reunionagenda = _mapper.Map<TAgendaDto, TAgenda>(reunionagenda);
                        reunion.ReunionAgendas.Add(_reunionagenda);
                        corrmax = nextCorrelativo(corrmax);
                    }
                }

                _context.TReunion.Update(reunion);
                //_context.TUsuario.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);

                var reudto = new TReunionDto();

                reudto.PlanAccion = request.PlanAccion;

                var updateList = new List<PlanVM>();
                var createList = new List<PlanVM>();
                foreach (var item in reudto.PlanAccion)
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

                //var r4 = await _planAccion.Create(createList, request.CodReunion);
                createList.ForEach(t => { t.docReferencia = request.CodReunion; t.docSubReferencia = request.CodReunion; });
                var r4 = await _mediator.Send(new CreatePlanAccionCommand() { planes = createList });

                //var r5 = await _planAccion.Update(updateList);
                var r5 = await _mediator.Send(new UpdatePlanAccionCommand() { planes = updateList });

                return Unit.Value;
            }

            public int nextCorrelativo(int corrmax)
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
