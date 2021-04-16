using System;
using System.Collections.Generic;
using MediatR;
using Hsec.Application.Common.Interfaces;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Domain.Entities.YoAseguro;
using Hsec.Application.YoAseguro.Models;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.YoAseguro.Commands.CreateYoAseguro
{
    public class CreateYoAseguroCommand : IRequest<Unit>
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

        //End Dto

        public class CreateYoAseguroCommandHandler : IRequestHandler<CreateYoAseguroCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public CreateYoAseguroCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(CreateYoAseguroCommand request, CancellationToken cancellationToken)
            {
                string codYoAseguro = request.CodYoAseguro;
                TYoAseguro yoAseguro = _context.TYoAseguro.Find(codYoAseguro);
                int quince = 14;
                string cat = "caotrce";

                if(yoAseguro == null)
                {
                    codYoAseguro = nextCod();
                    var entityYoA = new TYoAseguro();
                    entityYoA.CodYoAseguro = codYoAseguro;
                    entityYoA.CodPosGerencia = request.CodPosGerencia;
                    entityYoA.CodPersonaResponsable = request.CodPersonaResponsable;
                    entityYoA.Fecha = request.Fecha;
                    entityYoA.FechaEvalucion = request.FechaEvalucion;
                    entityYoA.ReportadosObservaciones = request.ReportadosObservaciones;
                    entityYoA.CorregidosObservaciones = request.CorregidosObservaciones;
                    entityYoA.ObsCriticaDia = request.ObsCriticaDia;
                    entityYoA.Calificacion = request.Calificacion;
                    entityYoA.Comentario = request.Comentario;
                    entityYoA.Reunion = request.Reunion;
                    entityYoA.Recomendaciones = request.Recomendaciones;
                    entityYoA.TituloReunion = request.TituloReunion;
                    entityYoA.TemaReunion = request.TemaReunion;
                    //entityYoA.PersonasReconocidas = _mapper.Map<ICollection<PersonaYoAseguroDto>, ICollection<TPersonaYoAseguro>>(request.PersonasReconocidas);
                    TPersonaYoAseguro per;
                    var personas = request.PersonasReconocidas;
                    foreach( var p in personas)
                    {
                        per = new TPersonaYoAseguro();
                        per.CodYoAseguro = codYoAseguro;
                        per.CodPersona = p.CodPersona;
                        entityYoA.PersonasReconocidas.Add(per);
                    }
                    _context.TYoAseguro.Add(entityYoA);
                }

                var createList = new List<PlanVM>();
                createList = request.PlanesAccion;

                //await _planAccion.Create(createList, codYoAseguro, codYoAseguro);
                createList.ForEach(t => { t.docReferencia = codYoAseguro; t.docSubReferencia = codYoAseguro; });
                var r1 = await _mediator.Send(new CreatePlanAccionCommand() { planes = createList });

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

            public string nextCod()
            {
                var COD_YOASEGURO_MAX = _context.TYoAseguro.Max(t => t.CodYoAseguro);
                if (COD_YOASEGURO_MAX == null) COD_YOASEGURO_MAX = "YOA00000001";
                else
                {
                    string numberStr = COD_YOASEGURO_MAX.Substring(3);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_YOASEGURO_MAX = String.Format("YOA{0,8:00000000}", max);
                }
                return COD_YOASEGURO_MAX;
            }
        }
    }
}
