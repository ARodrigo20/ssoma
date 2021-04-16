using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Personas.Queries.GetCode2Name
{

    public class GetCodPersonaNameQuery : IRequest<List<Tuple<string, string>>>
    {
        public List<string> data { get; set; }

        public class GetCodPersonaNameQueryHandler : IRequestHandler<GetCodPersonaNameQuery, List<Tuple<string, string>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCodPersonaNameQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<Tuple<string,string>>> Handle(GetCodPersonaNameQuery request, CancellationToken cancellationToken)
            {
                return  _context.TPersona
                    .Where(t => request.data.Contains(t.CodPersona))
                    .Select(p => new Tuple<string, string>(p.CodPersona, p.ApellidoPaterno + ' ' + p.ApellidoMaterno +' '+ p.Nombres))
                    .ToList();

            }
        }
    }
}