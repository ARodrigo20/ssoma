using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Observacion.Queries.GetPortletGrafico
{
    public class GetPortletGraficoQuery : IRequest<PortletMesesVM>
    {
        public string CodPersona { get; set; }
        public class GetPortletGraficoQueryHandler : IRequestHandler<GetPortletGraficoQuery, PortletMesesVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public GetPortletGraficoQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
            }
            
            const int COMPORTAMIENTO = 1;
            const int CONDICION = 2;
            const int TAREA = 3;
            const int IS = 4;

            public async Task<PortletMesesVM> Handle(GetPortletGraficoQuery request, CancellationToken cancellationToken)
            {
                // string user = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                // if (user == null) user = "admin";
                var vm = new PortletMesesVM();
                var fechaHoy = DateTime.Now.Year;
                var obs = _context.TObservaciones
                    .Where(d => d.Creado.Year == fechaHoy && (d.CodObservadoPor.Equals(request.CodPersona)) && d.Estado == true)
                    .GroupBy(t => new { t.CodTipoObservacion, t.FechaObservacion.Value.Month })
                    .Select(t => new { Tipo= t.Key.CodTipoObservacion , Month = t.Key.Month, Total = t.Count() })
                    .ToList();

                       

                foreach(var item in obs)
                {
                    switch (item.Tipo)
                    {
                        case TAREA:
                            vm.Tarea[item.Month-1] = item.Total;
                            break;
                        case COMPORTAMIENTO:
                            vm.Comportamiento[item.Month-1] = item.Total;
                            break;
                        case CONDICION:
                            vm.Condicion[item.Month-1] = item.Total;
                            break;
                        case IS:
                            vm.Interacion_Seguridad[item.Month-1] = item.Total;
                            break;
                    }
                        
                }
                return vm;
            }
        }

    }
}