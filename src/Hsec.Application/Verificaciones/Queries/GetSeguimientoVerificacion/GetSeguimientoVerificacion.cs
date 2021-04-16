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

namespace Hsec.Application.Verificaciones.Queries.GetSeguimientoVerificacion
{
    public class GetSeguimientoVerificacion : IRequest<int>
    {
        public string modulo { get; set; }
        public string persona { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }
        public string usuario {get;set;}
        public string rol {get;set;}
        public class GetSeguimientoVerificacionHandler : IRequestHandler<GetSeguimientoVerificacion, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSeguimientoVerificacionHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(GetSeguimientoVerificacion request, CancellationToken cancellationToken)
            {
                if(string.IsNullOrEmpty(request.modulo)) return 0;

                if(request.rol.Equals("6")){
                    if(request.modulo.Equals("12")){
                        return _context.TVerificaciones.Where(
                            t => t.Estado == true 
                            && (t.CreadoPor.Equals(request.usuario) || t.CodVerificacion.Equals(request.persona) )
                            && t.FechaVerificacion.Year.Equals(request.anio)
                            && t.FechaVerificacion.Month.Equals(request.mes)
                            )
                            .Count();
                    }
                    else{
                        string modulo;
                        if(request.modulo.Equals("12.01")) modulo = TipoVerificacion.IPERC_Continuo;
                        else if(request.modulo.Equals("12.02")) modulo = TipoVerificacion.PTAR;
                        else return 0;

                        return _context.TVerificaciones.Where(
                            t => t.Estado == true 
                            && ( t.CreadoPor.Equals(request.usuario) || t.CodVerificacion.Equals(request.persona) )
                            && t.CodTipoVerificacion.Equals(modulo) 
                            && t.FechaVerificacion.Year.Equals(request.anio)
                            && t.FechaVerificacion.Month.Equals(request.mes)
                            )
                            .Count();
                    }
                }
                else{
                    

                    if(request.modulo.Equals("12")){
                        return _context.TVerificaciones.Where(
                            t => t.Estado == true 
                            && t.CodVerificacion.Equals(request.persona) 
                            && t.FechaVerificacion.Year.Equals(request.anio)
                            && t.FechaVerificacion.Month.Equals(request.mes)
                            )
                            .Count();
                    }
                    else{
                        string modulo;
                        if(request.modulo.Equals("12.01")) modulo = TipoVerificacion.IPERC_Continuo;
                        else if(request.modulo.Equals("12.02")) modulo = TipoVerificacion.PTAR;
                        else return 0;

                        return _context.TVerificaciones.Where(
                            t => t.Estado == true 
                            && t.CodVerificacionPor.Equals(request.persona) 
                            && t.CodTipoVerificacion.Equals(modulo) 
                            && t.FechaVerificacion.Year.Equals(request.anio)
                            && t.FechaVerificacion.Month.Equals(request.mes)
                            )
                            .Count();
                    }


                    
                }
            }
        }
    }
}