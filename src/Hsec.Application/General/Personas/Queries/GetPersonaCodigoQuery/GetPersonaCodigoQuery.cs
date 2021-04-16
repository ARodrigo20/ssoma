using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Personas.Queries.GetPersonaCodigoQuery
{
    public class GetPersonaCodigoQuery : IRequest<string>
    {
        public string nroDocumento { get; set; }

        public class GetPersonaCodigoQueryHandler : IRequestHandler<GetPersonaCodigoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPersonaCodigoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<string> Handle(GetPersonaCodigoQuery request, CancellationToken cancellationToken)
            {
                var code = request.nroDocumento;
                var person = _context.TPersona.Where(t => t.NroDocumento.Equals(code)).FirstOrDefault();
                if(person != null) return person.CodPersona;
                else return "";
            }
        }
    }
}