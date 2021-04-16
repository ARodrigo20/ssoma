using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetPeligroRiesgo.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetPeligroRiesgo
{
    public class GetPeligroRiesgoQuery : IRequest<IList<GetPeligroRiesgoVM>>
    {
        public class GetPeligroRiesgoQueryHandler : IRequestHandler<GetPeligroRiesgoQuery, IList<GetPeligroRiesgoVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPeligroRiesgoQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IList<GetPeligroRiesgoVM>> Handle(GetPeligroRiesgoQuery request, CancellationToken cancellationToken)
            {
                //var peligroEntity = _context.TPeligro.Where(i=> i.Estado).ToList();
                //var riesgoEntity = _context.TRiesgo.Where(i=>i.Estado).ToList();

                var LISTA = (from riesgo in _context.TRiesgo
                             join peligro in _context.TPeligro on riesgo.CodPeligro equals peligro.CodPeligro into lista
                             from list in lista.DefaultIfEmpty()
                             where (list.CodTarea == null && !string.IsNullOrEmpty(riesgo.CodPeligro) && !string.IsNullOrEmpty(riesgo.CodRiesgo))
                             select new GetPeligroRiesgoVM
                             {
                                 codPeligro = riesgo.CodPeligro,
                                 descPeligro = list.DescripcionPeligro,
                                 codRiesgo = riesgo.CodRiesgo,
                                 descRiesgo = riesgo.Descripcion
                             }).OrderByDescending(i => i.codPeligro).ToList();
                           
                return LISTA;
            }
        }
    }
}