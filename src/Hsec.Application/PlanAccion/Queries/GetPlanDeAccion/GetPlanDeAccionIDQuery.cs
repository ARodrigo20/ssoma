using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class GetPlanAccionIDQuery : IRequest<PlanAccionMaestroDto>
    {
        public int CodAccion { get; set; }

        public GetPlanAccionIDQuery(int CodAccion)
        {
            this.CodAccion = CodAccion;
        }
        public class GetPlanAccionIDQueryHandler : IRequestHandler<GetPlanAccionIDQuery, PlanAccionMaestroDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            public GetPlanAccionIDQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<PlanAccionMaestroDto> Handle(GetPlanAccionIDQuery request, CancellationToken cancellationToken)
            {
                var datos = _context.TAccion.Include(i => i.RespPlanAccion).Where(i => i.CodAccion == request.CodAccion && i.Estado);

                var objeto = datos.AsQueryable()
                    .ProjectTo<PlanAccionMaestroDto>(_mapper.ConfigurationProvider)
                    .ToList().FirstOrDefault();

                PersonaVM respVM;
                foreach (var item in objeto.RespPlanAccion)
                {
                    respVM = new PersonaVM();
                    //respVM = await _persons.RequestNombApellidos(item.codPersona);
                    respVM = await _mediator.Send(new GetPersonaQuery() { CodPersona = item.codPersona });
                    item.nombres = respVM.Nombres;
                    item.apellidoPaterno = respVM.ApellidoPaterno;
                    item.apellidoMaterno = respVM.ApellidoMaterno;

                    var maxLev = _context.TLevantamientoPlan.Where(i => i.CodAccion == item.codAccion && i.CodPersona == i.CodPersona);
                    if(maxLev.Count() > 0)
                    {
                        item.porcentajeMayor = maxLev.Max(i => i.PorcentajeAvance);
                    }
                    //item.porcentajeMayor = maxLev;
                }
                return objeto;
            }
        }
    }
}
