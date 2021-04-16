using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs;
using Hsec.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries
{
    public class GetCursoProgramacionQuery : IRequest<GetCursoProgramacionVM>
    {
        public class GetCursoProgramacionQueryHandler : IRequestHandler<GetCursoProgramacionQuery, GetCursoProgramacionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCursoProgramacionQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetCursoProgramacionVM> Handle(GetCursoProgramacionQuery request, CancellationToken cancellationToken)
            {
                GetCursoProgramacionVM cursoProgramacion = new GetCursoProgramacionVM();
                                                                                  


                //var cursos = _context.TCurso.Where(i => i.Estado).ToList();

                var LISTA = (from curso in _context.TCurso.Where(i => i.Estado)
                             join temaCap in _context.TTemaCapacitacion.Where(i => i.Estado) on curso.CodTemaCapacita equals temaCap.CodTemaCapacita into lista
                             from list in lista.DefaultIfEmpty()
                             where (curso.Estado && list.Estado)
                             select new GetCursoProgramacionDto()
                             {
                                 codTemaCapacita = list.CodTemaCapacita,
                                 codTipoTema = list.CodTipoTema,
                                 codAreaCapacita = list.CodAreaCapacita,
                                 //codHha = list.CodHha,
                                 codEmpCapacita = curso.CodEmpCapacita,
                                 puntajeTotal = curso.PuntajeTotal,
                                 porcAprobacion = curso.PorcAprobacion,
                                 codCurso = curso.CodCurso,
                                 codLugarCapacita = curso.CodLugarCapacita,
                                 codSala = curso.CodSala,
                                 capacidad = curso.Capacidad,
                                 vigencia = curso.Vigencia,
                                 codVigenciaCapacita = curso.CodVigenciaCapacita,
                                 expositores = (from exp in curso.Expositores.Where(i => i.Estado)
                                                select new GetCursoProgramacionExpositorDto() {
                                    codPersona = exp.CodPersona,
                                    codCurso = exp.CodCurso,
                                    tipo = exp.Tipo                   
                                 }).ToList(),

                                 participantes = (from part in curso.Participantes.Where(i => i.Estado)
                                                  select new GetCursoProgramacionParticipanteDto()
                                                  {
                                                      codPersona = part.CodPersona,
                                                      codCurso = part.CodCurso,
                                                      nota = (!string.IsNullOrEmpty(part.Nota.ToString()) ? part.Nota.ToString() : ""),
                                                      tipo = part.Tipo
                                                  }).ToList(),
                                 
                                 fechaInicio = curso.FechaInicio,
                                 fechaFin = curso.FechaFin                                                    
                             }).ToList();

                foreach (var item in LISTA) {
                 
                    cursoProgramacion.data.Add(item);
                }
                cursoProgramacion.count = cursoProgramacion.data.Count();
                return cursoProgramacion;
            }
        }
    }
}
