using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetCode2Name;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries
{
    public class GetCursoProgramacionFiltroGenQuery : IRequest<GetCursoProgramacionFiltroGenVM>
    {
        public GetCursoProgramacionFiltroGenReqVM model { get; set; }
        public class GetCursoProgramacionFiltroGenQueryHandler : IRequestHandler<GetCursoProgramacionFiltroGenQuery, GetCursoProgramacionFiltroGenVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public readonly IMediator _mediator;

            public GetCursoProgramacionFiltroGenQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<GetCursoProgramacionFiltroGenVM> Handle(GetCursoProgramacionFiltroGenQuery request, CancellationToken cancellationToken)
            {
                GetCursoProgramacionFiltroGenVM VMResponse = new GetCursoProgramacionFiltroGenVM();
                var VM = request.model;
                DateTime fechFinVM = (DateTime)VM.fechaFin;
                VM.fechaFin = fechFinVM.AddDays(1).AddSeconds(-1);

                var SearchCursos = (from curso in _context.TCurso
                             join expo in _context.TExpositor on curso.CodCurso equals expo.CodCurso into expo
                             from m in expo.DefaultIfEmpty()
                              where 
                                (String.IsNullOrEmpty(VM.codigo) || curso.CodCurso.Equals(VM.codigo)) &&
                                (String.IsNullOrEmpty(VM.temaCap) || curso.CodTemaCapacita.Equals(VM.temaCap)) &&
                                (String.IsNullOrEmpty(VM.codPersona) || m.CodPersona.Equals(VM.codPersona)) &&
                                (
                                    (VM.fechaInicio <= curso.FechaInicio && curso.FechaInicio <= VM.fechaFin) 
                                    || (VM.fechaInicio <= curso.FechaFin && curso.FechaFin <= VM.fechaFin)
                                )
                             select new GetCursoProgramacionFiltroGenDto()
                             {
                                 codigo = curso.CodCurso,
                                 curso = curso.CodTemaCapacita,
                                 fecha = curso.FechaInicio,
                             })
                             .ToList();

                //var SearchCursos2 = _context.TCurso.Join(_context.TExpositor,
                //    jer => jer.CodCurso, per => per.CodCurso, (jer, per) => new { jer = jer, per = per })
                //    .ToList();

                //var SearchCursos3 = _context.TCurso.Join(_context.TExpositor, jer => jer.CodCurso, per => per.CodCurso, (jer, per) => new { jer = jer, per = per })
                //.Where(tuple => (tuple.jer.Estado && tuple.per.Estado &&
                //(String.IsNullOrEmpty(VM.codigo) || tuple.jer.CodCurso.Equals(VM.codigo)) &&
                //(String.IsNullOrEmpty(VM.temaCap) || tuple.jer.CodTemaCapacita.Equals(VM.temaCap)) &&
                //(String.IsNullOrEmpty(VM.codPersona) || tuple.per.CodPersona.Equals(VM.codPersona)) &&
                //((VM.fechaInicio <= tuple.jer.FechaInicio && tuple.jer.FechaInicio <= VM.fechaFin) || (VM.fechaInicio <= tuple.jer.FechaFin && tuple.jer.FechaFin <= VM.fechaFin))
                //))                
                //.Select(t => new GetCursoProgramacionFiltroGenDto()
                //{
                //    codigo = t.jer.CodCurso,
                //    curso = t.jer.CodTemaCapacita,
                //    fecha = t.jer.FechaInicio,                    
                //}
                //).ToList();

                var cursoPaginado = SearchCursos.OrderByDescending(i => i.fecha).Skip(VM.pagina * VM.paginaTamanio - VM.paginaTamanio).Take(VM.paginaTamanio).ToList();
                var temas = _context.TTemaCapacitacion.ToList();
                List<string> personas = new List<string>();
                foreach(var item in cursoPaginado) {
                    item.expositores = (from exp in _context.TExpositor.Where(i => i.Estado && i.CodCurso == item.codigo)
                                        select new GetCursoProgramacionFiltroGenExpositorDto()
                                        {
                                            codPersona = exp.CodPersona,
                                            codCurso = exp.CodCurso,
                                            tipo = exp.Tipo
                                        }).ToList();
                    item.participantes = (from part in _context.TParticipantes.Where(i => i.Estado && i.CodCurso == item.codigo)
                                          select new GetCursoProgramacionParticipanteDto()
                                          {
                                              codPersona = part.CodPersona,
                                              codCurso = part.CodCurso,
                                              nota = (!string.IsNullOrEmpty(part.Nota.ToString()) ? part.Nota.ToString() : ""),
                                              tipo = part.Tipo
                                          }).ToList();
                    foreach (var exp in item.expositores) personas.Add(exp.codPersona);
                    foreach (var par in item.participantes) personas.Add(par.codPersona);
                }
                //List<Tuple<string,string>> NombrePersonas = await _personasService.RequestNombApellidosList(personas.Distinct().ToList());
                List<Tuple<string, string>> NombrePersonas = await _mediator.Send(new GetCodPersonaNameQuery() { data = personas.Distinct().ToList() });

                foreach (var item in cursoPaginado) {
                    item.curso = temas.First(t => t.CodTemaCapacita == item.curso).Descripcion;
                    foreach (var exp in item.expositores)
                    {
                        if (NombrePersonas.Any(p => p.Item1 == exp.codPersona)) exp.nombres = NombrePersonas.FirstOrDefault(p => p.Item1 == exp.codPersona).Item2;
                        else exp.nombres = "";
                    }
                    foreach (var par in item.participantes) {
                        if (NombrePersonas.Any(p => p.Item1 == par.codPersona)) par.nombre = NombrePersonas.FirstOrDefault(p => p.Item1 == par.codPersona).Item2;
                        else par.nombre = "";
                    }
                    item.instructor= String.Join(", ", item.expositores.Select(e => e.nombres));
                    item.participantes.OrderBy(a => a.nombre);
                }
                VMResponse.data = cursoPaginado;
                VMResponse.count = SearchCursos.Count();
                return VMResponse;
            }
        }
    }
}