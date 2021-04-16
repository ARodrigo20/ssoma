using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.General.Personas.Queries.GetPersona;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Hsec.Application.General.Personas.Queries.GetPersonasQuery
{
    public class GetPersonasQuery : IRequest<List<PersonaVM>>
    {
        public ICollection<string> dnisPersona { get; set; }
        public class GetPersonasQueryHandler : IRequestHandler<GetPersonasQuery, List<PersonaVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPersonasQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<PersonaVM>> Handle(GetPersonasQuery request, CancellationToken cancellationToken)
            {
                List<PersonaVM> personas = new List<PersonaVM>();

                foreach (var dni in request.dnisPersona)
                {
                    var persona = _context.TPersona.Where(p => p.Estado && p.NroDocumento.Equals(dni));
                    if (persona.Count() > 0)
                    {
                        personas.Add(_mapper.Map<PersonaVM>(persona.First()));
                    }
                }
                return personas;
            }
        }
    }
}