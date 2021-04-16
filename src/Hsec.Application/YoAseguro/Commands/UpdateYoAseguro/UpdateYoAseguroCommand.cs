using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.YoAseguro;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.YoAseguro.Models;
using Hsec.Application.Common.Models;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion;

namespace Hsec.Application.YoAseguro.Commands.UpdateYoAseguro
{
    public class UpdateYoAseguroCommand : IRequest<Unit>
    {
        //Dto
        public string CodYoAseguro { get; set; }
        public string CodPosGerencia { get; set; }
        public string CodPersonaResponsable { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? FechaEvalucion { get; set; }
        public int ReportadosObservaciones { get; set; }
        public int CorregidosObservaciones { get; set; }
        public string ObsCriticaDia { get; set; }
        public string Calificacion { get; set; }
        public string Comentario { get; set; }
        public string Reunion { get; set; }
        public string Recomendaciones { get; set; }
        public string TituloReunion { get; set; }
        public string TemaReunion { get; set; }
        public ICollection<PersonaYoAseguroDto> PersonasReconocidas { get; set; }
        public List<PlanVM> PlanesAccion { get; set; }

        public class UpdateYoAseguroCommandHandler : IRequestHandler<UpdateYoAseguroCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public UpdateYoAseguroCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateYoAseguroCommand request, CancellationToken cancellationToken)
            {
                TYoAseguro entityYoA = _context.TYoAseguro.Find(request.CodYoAseguro);

                if (entityYoA != null && entityYoA.Estado == false) throw new NotFoundException("YoAseguro", request.CodYoAseguro);
                else
                {
                    entityYoA.CodPosGerencia = request.CodPosGerencia;
                    entityYoA.CodPersonaResponsable = request.CodPersonaResponsable;
                    //entityYoA.Fecha = request.Fecha;
                    //entityYoA.FechaEvalucion = request.FechaEvalucion;
                    entityYoA.ReportadosObservaciones = request.ReportadosObservaciones;
                    entityYoA.CorregidosObservaciones = request.CorregidosObservaciones;
                    entityYoA.ObsCriticaDia = request.ObsCriticaDia;
                    entityYoA.Calificacion = request.Calificacion;
                    entityYoA.Comentario = request.Comentario;
                    entityYoA.Reunion = request.Reunion;
                    entityYoA.Recomendaciones = request.Recomendaciones;
                    entityYoA.TituloReunion = request.TituloReunion;
                    entityYoA.TemaReunion = request.TemaReunion;
                    entityYoA.PersonasReconocidas = _context.TPersonaYoAseguro.Where(t => t.CodYoAseguro.Equals(request.CodYoAseguro)).ToList();

                    foreach (var item in entityYoA.PersonasReconocidas)
                    {
                        _context.TPersonaYoAseguro.Remove(item);
                    }
                    TPersonaYoAseguro per;
                    var personas = request.PersonasReconocidas;
                    foreach (var p in personas)
                    {
                        per = new TPersonaYoAseguro();
                        per.CodYoAseguro = request.CodYoAseguro;
                        per.CodPersona = p.CodPersona;
                        entityYoA.PersonasReconocidas.Add(per);
                    }

                    var createList = new List<PlanVM>();
                    var updateList = new List<PlanVM>();

                    foreach(var plan in request.PlanesAccion)
                    {
                        if (plan.codAccion < 0)
                        {
                            createList.Add(plan);
                        }
                        else
                        {
                            updateList.Add(plan);
                        }
                    }


                    //var r1 = await _planAccion.Create(createList, request.CodYoAseguro, request.CodYoAseguro);
                    createList.ForEach(t => { t.docReferencia = request.CodYoAseguro; t.docSubReferencia = request.CodYoAseguro; });
                    var r1 = await _mediator.Send(new CreatePlanAccionCommand() { planes = createList });
                    //var r2 = await _planAccion.Update(updateList);
                    var r2 = await _mediator.Send(new UpdatePlanAccionCommand() { planes = updateList });

                    _context.TYoAseguro.Update(entityYoA);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
