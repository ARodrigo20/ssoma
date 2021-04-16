using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetCartillasPorPerson
{
    public class GetVerCCPorPersonasQuery : IRequest<VerCCPorPersonaVM>
    {
        public string CodReferencia {get;set;}
        public string CodPersona { get; set; }
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public class GetVerCCPorPersonasQueryHandler : IRequestHandler<GetVerCCPorPersonasQuery, VerCCPorPersonaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetVerCCPorPersonasQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<VerCCPorPersonaVM> Handle(GetVerCCPorPersonasQuery request, CancellationToken cancellationToken)
            {
                var vm = new VerCCPorPersonaVM();

                vm.list = _context.TPlanAnualVerConCri.Where(t => 
                    t.Anio.Equals(request.Anio)
                    && ( String.IsNullOrEmpty(request.CodMes) || t.CodMes.Equals(request.CodMes))
                    && ( String.IsNullOrEmpty(request.CodPersona) ||  t.CodPersona.Equals(request.CodPersona) )
                    && ( String.IsNullOrEmpty(request.CodReferencia) ||  t.CodReferencia.Equals(request.CodReferencia) )
                    )
                    .ProjectTo<VerCCProDto>(_mapper.ConfigurationProvider)
                    .ToList();

                return vm;
            }
        }
    }
}