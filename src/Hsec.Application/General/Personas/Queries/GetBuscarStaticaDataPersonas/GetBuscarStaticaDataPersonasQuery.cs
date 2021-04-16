using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Personas.Queries.GetBuscarStaticaDataPersonas
{
    public class GetBuscarStaticaDataPersonasQuery : IRequest<StaticaDataPersonasVM>
    {

        public class GetTodosQueryHandler : IRequestHandler<GetBuscarStaticaDataPersonasQuery, StaticaDataPersonasVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<StaticaDataPersonasVM> Handle(GetBuscarStaticaDataPersonasQuery request, CancellationToken cancellationToken)
            {

                var vm = new StaticaDataPersonasVM();

                vm.ListTipoDocumentos = Enum.GetValues(typeof(TipoDocumento))
                        .Cast<TipoDocumento>()
                        .Select(p => new TipoDocumentoDto { Value = (int)p, Name = p.ToString() })
                        .ToList();

                vm.ListTipoPersonas = Enum.GetValues(typeof(TipoPersona))
                        .Cast<TipoPersona>()
                        .Select(p => new TipoPersonaDto { Value = (int)p, Name = p.ToString() })
                        .ToList();

                return vm;
            }
        }
    }
}
