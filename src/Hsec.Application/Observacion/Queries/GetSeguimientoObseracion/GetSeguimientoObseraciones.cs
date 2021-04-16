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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Observacion.Queries.GetSeguimientoObseraciones
{
    public class GetSeguimientoObseraciones : IRequest<List<Tuple<string,int>>>
    {
        public string modulo { get; set; }
        public List<string> personas { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }
        public class GetSeguimientoObseracionHandler : IRequestHandler<GetSeguimientoObseraciones, List<Tuple<string, int>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSeguimientoObseracionHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<Tuple<string, int>>> Handle(GetSeguimientoObseraciones request, CancellationToken cancellationToken)
            {
                List<int> opt = new List<int>();
                switch (request.modulo) {
                    case "01.04":
                        opt.Add(1);
                        opt.Add(2);
                        break;
                    case "01.01":
                        opt.Add(3);
                        break;
                    case "01.05":
                        opt.Add(4);
                        break;
                }
                return _context.TObservaciones.Where(
                        t => t.Estado == true
                        && request.personas.Contains(t.CodObservadoPor)
                        && t.FechaObservacion.Value.Year.Equals(request.anio)
                        && t.FechaObservacion.Value.Month.Equals(request.mes)
                        && (opt.Contains(t.CodTipoObservacion) && (t.CodSubTipoObs == "1" || t.CodSubTipoObs == "2" || t.CodSubTipoObs == "3" || t.CodSubTipoObs == null || !opt.Contains(4)))
                        ).GroupBy(t => t.CodObservadoPor).Select(g => new Tuple<string, int>(g.Key, g.Count())).ToList();
            }
        }
    }
}

//lo codigos de los modulos son personalisados para la vista no guardan correlacion con el tipo de observacion
//if (request.modulo.Equals("01.04")) // acto condicion - comportamiento y condicion
//{
//    resp = _context.TObservaciones.Where(
//        t => t.Estado == true
//        && t.CodObservadoPor.Equals(request.persona)
//        && t.Creado.Year.Equals(request.anio)
//        && t.Creado.Month.Equals(request.mes)
//        && (t.CodTipoObservacion == 1 || t.CodTipoObservacion == 2)
//        )
//        .Count();
//}
//if (request.modulo.Equals("01.01")) // Tarea
//{
//    resp = _context.TObservaciones.Where(
//        t => t.Estado == true
//        && t.CodObservadoPor.Equals(request.persona)
//        && t.Creado.Year.Equals(request.anio)
//        && t.Creado.Month.Equals(request.mes)
//        && (t.CodTipoObservacion == 3)
//        )
//        .Count();
//}
//if (request.modulo.Equals("01.05")) // interaccion seguridad
//{
//    resp = _context.TObservaciones.Where(
//        t => t.Estado == true
//        && t.CodObservadoPor.Equals(request.persona)
//        && t.Creado.Year.Equals(request.anio)
//        && t.Creado.Month.Equals(request.mes)
//        && (t.CodTipoObservacion == 4)
//        )
//        .Count();
//}

//return resp;

//if(request.modulo.Equals("08")){
//    return _context.TObservaciones.Where(
//        t => t.Estado == true 
//        && t.CodObservadoPor.Equals(request.persona) 
//        && t.FechaObservacion.Value.Year.Equals(request.anio)
//        && t.FechaObservacion.Value.Month.Equals(request.mes)
//        )
//        .Count();
//}
//else{
//    TipoObservacion modulo;
//    if(request.modulo.Equals("08.01")) modulo = TipoObservacion.Comportamiento;
//    else if(request.modulo.Equals("08.02")) modulo = TipoObservacion.Condicion;
//    else if(request.modulo.Equals("08.03")) modulo = TipoObservacion.Tarea;
//    else if(request.modulo.Equals("08.04")) modulo = TipoObservacion.Iteraccion_Seguridad;
//    else if(request.modulo.Equals("08.05")) modulo = TipoObservacion.VerificacionControlCritico;
//    else return 0;

//    return _context.TObservaciones.Where(
//        t => t.Estado == true 
//        && t.CodObservadoPor.Equals(request.persona) 
//        && t.CodTipoObservacion.Equals(modulo) 
//        && t.FechaObservacion.Value.Year.Equals(request.anio)
//        && t.FechaObservacion.Value.Month.Equals(request.mes)
//        )
//        .Count();
//}