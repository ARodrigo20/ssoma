using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.VerificacionObservacionCc.Queries.GetEjecutadosQuery
{
    public class GetEjecutadosQuery : IRequest<EjecutadosVM>
    {
        public string CodPersona { get; set; }
        //public string CodReferencia { get; set; }
        public string Anio { get; set; }
        public string CodMes { get; set; }
        public class GetEjecutadosQueryHandler : IRequestHandler<GetEjecutadosQuery, EjecutadosVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetEjecutadosQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<EjecutadosVM> Handle(GetEjecutadosQuery request, CancellationToken cancellationToken)
            {
                //var vm = new EjecutadosVM();

                //var filerListQuery = (from ver in _context.TObservacionVerControlCritico
                //                      join obs in _context.TObservaciones on ver.CodObservacion equals obs.CodObservacion into gj
                //                      from x in gj.DefaultIfEmpty()
                //                      where (
                //                        /*obs.Estado
                //                        &&*/ (ver.CodObservadoPor.Equals(request.CodPersona))
                //                      && (ver.Fecha.Year == Int32.Parse(request.Anio))
                //                      && (request.CodMes == null || ver.Fecha.Month == Int32.Parse(request.CodMes))
                //                      && (ver.CodCartilla.Equals(request.CodReferencia))
                //                      )
                //                      group ver by new { ver.CodObservadoPor, ver.CodCartilla } into list
                //                      select new EjecutadoDto
                //                      {
                //                          CodPersona = list.Key.CodObservadoPor,
                //                          CodCartilla = list.Key.CodCartilla,
                //                          Valor = list.Count()
                //                      });

                //var ListQuery = filerListQuery
                //    .ToList();


                //vm.List = ListQuery;

                //return vm;

                var vm = new EjecutadosVM();

                var filerListQuery = (from ver in _context.TObservacionVerControlCritico
                                      join obs in _context.TObservaciones on ver.CodObservacion equals obs.CodObservacion into ob
                                      from x in ob.DefaultIfEmpty()
                                      where (
                                          x.Estado
                                          && (x.CodObservadoPor.Equals(request.CodPersona))
                                          && (x.FechaObservacion.Value.Year == Int32.Parse(request.Anio))
                                          && (request.CodMes == null || x.FechaObservacion.Value.Month == Int32.Parse(request.CodMes))
                                          /*&& (ver.CodCartilla.Equals(request.CodReferencia))*/
                                      )
                                      group ver by new { ver.CodObservadoPor, ver.CodCartilla } into list
                                      select new EjecutadoDto
                                      {
                                          CodPersona = list.Key.CodObservadoPor,
                                          CodCartilla = list.Key.CodCartilla,
                                          Valor = list.Count()
                                      });

                //var result = filerListQuery
                //    .First();

                //return (filerListQuery.Count() > 0 ) ? filerListQuery.First().Valor : 15;
                vm.List = filerListQuery.ToList();
                return vm;
            }
        }
    }
}