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

namespace Hsec.Application.Incidentes.Queries.GetCabeceraIncidente
{
    public class GetCabeceraIncidenteQuery : IRequest<IncidenteCabeceraVM>
    {
        public string CodIncidente { get; set; }
        public class GetCabeceraIncidenteQueryHandler : IRequestHandler<GetCabeceraIncidenteQuery, IncidenteCabeceraVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCabeceraIncidenteQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IncidenteCabeceraVM> Handle(GetCabeceraIncidenteQuery request, CancellationToken cancellationToken)
            {
                var vm = new IncidenteCabeceraVM();

                vm = _context.TIncidente.Where(t => t.CodIncidente.Equals(request.CodIncidente))
                    .ProjectTo<IncidenteCabeceraVM>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();
                //if (incidente == null) return vm;
                //vm.AreaHSEC = incidente.CodPosicionGer;
                //vm.Tipo = incidente.CodPosicionSup;
                //vm.SuperIntendencia = incidente.CodPosicionSup;
                //vm.Tipo = incidente.CodAreaHsec;

                return vm;
            }
        }

    }
}