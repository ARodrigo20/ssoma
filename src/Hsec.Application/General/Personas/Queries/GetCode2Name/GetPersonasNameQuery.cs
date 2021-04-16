using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Personas.Queries.GetCode2Name
{

    public class GetPersonasNameQuery : IRequest<List<PersonaMinDTO>>
    {
        public List<string> data { get; set; }

        public class GetPersonasNameQueryHandler : IRequestHandler<GetPersonasNameQuery, List<PersonaMinDTO>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPersonasNameQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<PersonaMinDTO>> Handle(GetPersonasNameQuery request, CancellationToken cancellationToken)
            {
                return  _context.TPersona
                    .Where(t => request.data.Contains(t.CodPersona))
                    .Select(p => new PersonaMinDTO { 
                    CodPersona=p.CodPersona,
                    NroDNI= p.NroDocumento,
                    Nombres = p.ApellidoPaterno + ' ' + p.ApellidoMaterno + ' ' + p.Nombres,
                    Empresa= p.Empresa
                    })
                    .ToList();

            }
        }
    }
}