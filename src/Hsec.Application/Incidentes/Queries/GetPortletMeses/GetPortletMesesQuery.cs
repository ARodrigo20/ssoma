using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Microsoft.AspNetCore.Http;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using System.Reflection;
using System.Data.SqlTypes;
using System.Collections.Generic;
//using System.Data.Entity.SqlServer;

namespace Hsec.Application.Incidentes.Queries.GetPortletMeses
{
    public class GetPortletMesesQuery : IRequest<List<Portlet>>
    {
        public class GetPortletMesesQueryHandler : IRequestHandler<GetPortletMesesQuery, List<Portlet>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public GetPortletMesesQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
            }

            public async Task<List<Portlet>> Handle(GetPortletMesesQuery request, CancellationToken cancellationToken)
            {
                string user = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (user == null) user = "admin";

                var today = DateTime.Today;

                var trendData =
                    (from d in _context.TIncidente
                     where d.FechaDelSuceso.Value.Year == today.Year && (d.CreadoPor.Equals(user) || user.Equals("admin"))
                     group d by new
                     {
                         Year = d.FechaDelSuceso.Value.Year,
                         Month = d.FechaDelSuceso.Value.Month
                     } into g
                     select new
                     {
                         Year = g.Key.Year,
                         Month = g.Key.Month,
                         Total = g.Count()
                     }
               ).AsEnumerable()
                .Select(g => new Portlet {
                    Mes = g.Month,
                    Total = g.Total
                }).ToList();

                return trendData;
            }
        }
    }
}