using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries
{
    public class GetCursoPosicionQuery : IRequest<IList<GetCursoPosicionResponseVM>>
    {
        public IList<string> codElipses { get; set; }
        public class GetCursoPosicionQueryHandler : IRequestHandler<GetCursoPosicionQuery, IList<GetCursoPosicionResponseVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCursoPosicionQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IList<GetCursoPosicionResponseVM>> Handle(GetCursoPosicionQuery request, CancellationToken cancellationToken)
            {
                IList<string> codigosElipse = request.codElipses;
                var innerJoin = (from plan in _context.TPlanTema.Where(i => i.Estado)
                                join temaCap in _context.TTemaCapacitacion.Where(i => i.Estado) on plan.CodTemaCapacita equals temaCap.CodTemaCapacita
                                into lista from list in lista.DefaultIfEmpty()
                                where (plan.Estado && list.Estado && codigosElipse.Contains(plan.CodReferencia))
                                select new GetCursoPosicionResponseVM
                                {
                                    competencia = list.CompetenciaHs,
                                    codTemaCapacita = list.CodTemaCapacita,
                                    curso = list.Descripcion,
                                    tipo = plan.Tipo,
                                    codTipo = list.CodTipoTema,
                                    codPosicion = plan.CodReferencia,
                                    bd = true
                                }).OrderByDescending(i => i.codTemaCapacita);

                IList<GetCursoPosicionResponseVM> getCursosPos = new List<GetCursoPosicionResponseVM>();               
                foreach (var item in innerJoin)
                {
                    getCursosPos.Add(item);
                }
                return getCursosPos;
            }
        }
    }
}
