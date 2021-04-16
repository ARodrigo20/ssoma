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

namespace Hsec.Application.VerificacionControlCritico.Queries.GetVerificacionControlCritico
{
    public class GetVerificacionControlCriticoQuery : IRequest<GetVerConCriVM>
    {
        public string Codigo { get; set; }
        public class GetVerificacionControlCriticoQueryHandler : IRequestHandler<GetVerificacionControlCriticoQuery, GetVerConCriVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetVerificacionControlCriticoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetVerConCriVM> Handle(GetVerificacionControlCriticoQuery request, CancellationToken cancellationToken)
            {
                try{

                    var codigo = request.Codigo;
                    GetVerConCriVM VM = new GetVerConCriVM();
                    VM.data = _context.TVerificacionControlCritico
                        .Where(t => t.Estado == true && t.CodigoVCC.Equals(codigo))
                        .ProjectTo<VerificacionControlCriticoDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefault();
                        
                    if(VM.data!=null){
                        VM.data.Criterios = _context.TVerificacionControlCriticoCartilla
                            .Where(t => t.Estado == true && t.CodigoVCC.Equals(codigo))
                            .ProjectTo<CriterioEvaluacionDto>(_mapper.ConfigurationProvider)
                            .ToList();
                    }

                    return VM;
                }
                catch(Exception ex){
                    throw ex;
                }
            }
        }
    }
}