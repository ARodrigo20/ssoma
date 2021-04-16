using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Verificaciones.Models;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Verificaciones.Queries.GetVerificacion
{
    
    public class GetVerificacionQuery : IRequest<GetVerificacionVM> 
    {
        public string CodVerificacion { get; set; }
        public class GetVerificacionQueryHandler : IRequestHandler<GetVerificacionQuery, GetVerificacionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetVerificacionQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetVerificacionVM> Handle(GetVerificacionQuery request, CancellationToken cancellationToken)
            {
                var vm = new GetVerificacionVM();
                vm.data = _context.TVerificaciones.Where(t => 1==1 
                    && t.Estado == true 
                    && t.CodVerificacion.Equals(request.CodVerificacion))
                    .ProjectTo<VerificacionDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();

                var tipo = vm.data.CodTipoVerificacion;
                if(tipo.Equals(TipoVerificacion.IPERC_Continuo)){
                    vm.data.VerificacionIPERC = _context.TVerificacionIPERC.Where(t => 1==1 
                        && t.Estado == true
                        && t.CodVerificacion.Equals(request.CodVerificacion))
                    .ProjectTo<VerificacionIPERCDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();
                }
                else if(tipo.Equals(TipoVerificacion.PTAR)){
                    vm.data.VerificacionPTAR = _context.TVerificacionPTAR.Where(t => 1==1 
                        && t.Estado == true
                        && t.CodVerificacion.Equals(request.CodVerificacion))
                    .ProjectTo<VerificacionPTARDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();
                }
                return vm;
            }
        }

    }
}