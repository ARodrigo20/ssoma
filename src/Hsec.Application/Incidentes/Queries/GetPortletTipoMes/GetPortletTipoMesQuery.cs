using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Incidentes.Queries.GetPortletTipoMes
{
    public class GetPortletTipoMesQuery : IRequest<List<PortletTipoIncidente>>
    {
        public class GetPortletTipoMesHandler : IRequestHandler<GetPortletTipoMesQuery, List<PortletTipoIncidente>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public GetPortletTipoMesHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
            }

            public async Task<List<PortletTipoIncidente>> Handle(GetPortletTipoMesQuery request, CancellationToken cancellationToken)
            {
                TIncidente temp = new TIncidente();
                temp.CodTipoIncidente = "";
                string user = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (user == null) user = "admin";

                var today = DateTime.Today;

                var trendData =
                    (from ti in _context.TIncidente
                     where ti.FechaDelSuceso.Value.Year == today.Year 
                            && ti.FechaDelSuceso.Value.Month == today.Month
                            && (ti.CreadoPor.Equals(user))
                     group ti by new
                     {
                         TipoIncidente = ti.CodTipoIncidente
                         //,SubTipoIncidente = ti.CodSubTipoIncidente
                     } into g
                     select new
                     {
                         CodTipoIncidente = g.Key.TipoIncidente,
                         //SubTipoIncidente = g.Key.SubTipoIncidente,
                         Total = g.Count()
                     }
               ).AsEnumerable()
                .Select(g => new PortletTipoIncidente
                {
                    CodTipoIncidente = g.CodTipoIncidente,
                    Total = g.Total
                }).ToList();

                return trendData;
            }
        }

    }
}