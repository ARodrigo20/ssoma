using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;
using Hsec.Application.General.Personas.Queries.GetCode2Name;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries
{
    public class GetCursoProgramacionIDQuery : IRequest<GetCursoProgramacionIDDto>
    {
        public string codCurso { get; set; } //codigo

        public class GetCursoProgramacionIDQueryHandler : IRequestHandler<GetCursoProgramacionIDQuery, GetCursoProgramacionIDDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetCursoProgramacionIDQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<GetCursoProgramacionIDDto> Handle(GetCursoProgramacionIDQuery request, CancellationToken cancellationToken)
            {
                string codCurso = request.codCurso;
                
                var LISTA = (from curso in _context.TCurso.Where(i => i.Estado)
                             join temaCap in _context.TTemaCapacitacion.Where(i => i.Estado) on curso.CodTemaCapacita equals temaCap.CodTemaCapacita into lista
                             from list in lista.DefaultIfEmpty()
                             where (curso.Estado && list.Estado && curso.CodCurso == codCurso)
                             select new GetCursoProgramacionIDDto()
                             {
                                 id = curso.CodCurso,
                                 //title = list.Descripcion,
                                 codTemaCapacita = curso.CodTemaCapacita,
                                 estado = curso.Estado,
                                 //start = curso.FechaInicio,
                                 //end = curso.FechaFin,
                                 recurrenceId = curso.RecurrenceID,
                                 capacidad = curso.Capacidad,
                                 codAreaCapacita = curso.CodAreaCapacita,
                                 codEmpCapacita = curso.CodEmpCapacita,
                                 //codHha = curso.CodHha,
                                 codLugarCapacita = curso.CodLugarCapacita,
                                 codSala = curso.CodSala,
                                 codTipoTema = curso.CodTipoTema,
                                 codVigenciaCapacita = curso.CodVigenciaCapacita,
                                 //descripcionLugar = curso.DescripcionLugar,
                                 fechaFin = curso.FechaFin,
                                 fechaInicio = curso.FechaInicio,
                                 porcAprobacion = curso.PorcAprobacion,
                                 puntajeTotal = curso.PuntajeTotal,
                                 //tipoVigencia = curso.TipoVigencia,
                                 vigencia = curso.Vigencia,
                                 enlace= curso.Enlace,
                                 online= curso.Online,
                                 duracion=curso.Duracion,
                                 expositores = (from exp in curso.Expositores.Where(i => i.Estado)
                                                select new GetCursoProgramacionExpositorDto()
                                                {
                                                    codPersona = exp.CodPersona,
                                                    codCurso = exp.CodCurso,
                                                    tipo = exp.Tipo
                                                }).ToList(),

                                 participantes = (from part in curso.Participantes.Where(i => i.Estado)
                                                  select new GetCursoProgramacionParticipanteDto()
                                                  {
                                                      codPersona = part.CodPersona,
                                                      codCurso = part.CodCurso,
                                                      intentos= part.Intentos,
                                                      nota = (part.Nota != null ? part.Nota.ToString() : ""),
                                                      tipo = part.Tipo                                                 
                                                  }).ToList(),                                 
                             }).FirstOrDefault();

                PersonaVM respExp;
                //GetResponseNombApVM respExp;
                foreach (var list in LISTA.expositores) {
                    //respExp = new GetResponseNombApVM();
                    respExp = new PersonaVM();
                    respExp.Nombres = "";
                    respExp.ApellidoPaterno = "";
                    respExp.ApellidoMaterno = "";
                    var codPersona = list.codPersona;
                    if (!string.IsNullOrEmpty(codPersona))
                    {
                        //respExp = await _persons.RequestNombApellidos(codPersona);
                        respExp = await _mediator.Send(new GetPersonaQuery() { CodPersona = codPersona });
                    }

                    if (respExp != null)
                    {
                        list.nombre = respExp.ApellidoPaterno + " " + respExp.ApellidoMaterno + ", " + respExp.Nombres;
                    }
                }

                GetResponseNombApPartVM respPart;

                //var PersonasCap = await _persons.RequestPersonasCap(LISTA.participantes.Select(p=>p.codPersona).ToList());
                var PersonasCap = await _mediator.Send(new GetPersonasNameQuery() { data = LISTA.participantes.Select(p => p.codPersona).ToList() });

                foreach (var list in LISTA.participantes)
                {
                    var Persona = PersonasCap.Find(p => p.CodPersona == list.codPersona);
                    list.nombre = Persona.Nombres; 
                    list.dni = Persona.NroDNI;
                    list.empresa = Persona.Empresa;                    
                }
                return LISTA;
            }
        }
    }
}
