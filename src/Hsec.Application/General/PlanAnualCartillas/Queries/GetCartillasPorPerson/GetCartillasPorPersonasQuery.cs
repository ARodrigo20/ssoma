using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.PlanAnualCartillas.Queries.GetCartillasPorPerson
{
    public class GetCartillasPorPersonasQuery : IRequest<CartillaPorPersonaVM>
    {
        public string CodPersona { get; set; }
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public class GetCartillasPorPersonasQueryHandler : IRequestHandler<GetCartillasPorPersonasQuery, CartillaPorPersonaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            // private readonly 

            public GetCartillasPorPersonasQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CartillaPorPersonaVM> Handle(GetCartillasPorPersonasQuery request, CancellationToken cancellationToken)
            {
                var vm = new CartillaPorPersonaVM();

                var list = _context.TPlanAnual.Where(t => 
                    t.Anio.Equals(request.Anio)
                    && t.CodMes.Equals(request.CodMes)
                    && t.CodPersona.Equals(request.CodPersona)
                    )
                    .ProjectTo<CartillasProDto>(_mapper.ConfigurationProvider)
                    .ToList();
                
                // var referenciaLista = new List<string>();
                // foreach (var item in list)
                // {
                //     var tamanio = Int32.Parse(item.Valor);
                //     for (int i = 0; i < tamanio; i++)
                //     {
                //         referenciaLista.Add(item.CodReferencia);
                //     }
                // }
                vm.list = list;
                return vm;
            }
        }
    }
}