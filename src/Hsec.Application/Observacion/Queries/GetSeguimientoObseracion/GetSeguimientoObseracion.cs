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

namespace Hsec.Application.Observacion.Queries.GetSeguimientoObseracion
{
    public class GetSeguimientoObseracion : IRequest<int>
    {
        public string modulo { get; set; }
        public string persona { get; set; }
        public int anio { get; set; }
        public int mes { get; set; }
        public string usuario {get;set;}
        public string rol {get;set;}
        public class GetSeguimientoObseracionHandler : IRequestHandler<GetSeguimientoObseracion, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSeguimientoObseracionHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(GetSeguimientoObseracion request, CancellationToken cancellationToken)
            {
                if(string.IsNullOrEmpty(request.modulo)) return 0;

                var resp = 0;

                if (request.rol.Equals("6"))
                {
                    if (request.modulo.Equals("01.04")) // acto condicion - comportamiento y condicion
                    {
                        resp = _context.TObservaciones.Where(
                            t => t.Estado == true
                            && (t.CreadoPor.Equals(request.usuario) || t.CodObservadoPor.Equals(request.persona) )
                            && t.FechaObservacion.Value.Year.Equals(request.anio)
                            && t.FechaObservacion.Value.Month.Equals(request.mes)
                            && (t.CodTipoObservacion == 1 || t.CodTipoObservacion == 2)
                            )
                            .Count();
                    }
                    if (request.modulo.Equals("01.01")) // Tarea
                    {
                        resp = _context.TObservaciones.Where(
                            t => t.Estado == true
                            && (t.CreadoPor.Equals(request.usuario) || t.CodObservadoPor.Equals(request.persona) )
                            && t.FechaObservacion.Value.Year.Equals(request.anio)
                            && t.FechaObservacion.Value.Month.Equals(request.mes)
                            && (t.CodTipoObservacion == 3)
                            )
                            .Count();
                    }
                    if (request.modulo.Equals("01.05")) // interaccion seguridad
                    {
                        resp = _context.TObservaciones.Where(
                            t => t.Estado == true
                            && (t.CreadoPor.Equals(request.usuario) || t.CodObservadoPor.Equals(request.persona) )
                            && t.FechaObservacion.Value.Year.Equals(request.anio)
                            && t.FechaObservacion.Value.Month.Equals(request.mes)
                            && (t.CodTipoObservacion == 4)
                            )
                            .Count();
                    }
                }
                else{

                //lo codigos de los modulos son personalisados para la vista no guardan correlacion con el tipo de observacion
                    if (request.modulo.Equals("01.04")) // acto condicion - comportamiento y condicion
                    {
                        resp = _context.TObservaciones.Where(
                            t => t.Estado == true
                            && t.CodObservadoPor.Equals(request.persona)
                            && t.FechaObservacion.Value.Year.Equals(request.anio)
                            && t.FechaObservacion.Value.Month.Equals(request.mes)
                            && (t.CodTipoObservacion == 1 || t.CodTipoObservacion == 2)
                            )
                            .Count();
                    }
                    if (request.modulo.Equals("01.01")) // Tarea
                    {
                        resp = _context.TObservaciones.Where(
                            t => t.Estado == true
                            && t.CodObservadoPor.Equals(request.persona)
                            && t.FechaObservacion.Value.Year.Equals(request.anio)
                            && t.FechaObservacion.Value.Month.Equals(request.mes)
                            && (t.CodTipoObservacion == 3)
                            )
                            .Count();
                    }
                    else
                    {
                        resp = _context.TObservaciones.Where(
                            t => t.Estado == true
                            && t.CodObservadoPor.Equals(request.persona)
                            && t.FechaObservacion.Value.Year.Equals(request.anio)
                            && t.FechaObservacion.Value.Month.Equals(request.mes)
                            && (t.CodTipoObservacion == 4)
                            )
                            .Count();
                    }

                }
                return resp;

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


            }
        }
    }
}