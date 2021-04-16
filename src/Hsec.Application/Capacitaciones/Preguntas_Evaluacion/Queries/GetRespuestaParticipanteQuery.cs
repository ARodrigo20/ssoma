using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Queries
{   
    public class GetRespuestaParticipanteQuery : IRequest<IList<PreguntaRespuestaVM>>
    {
        public string codCurso { get; set; }
        public string codPersona { get; set; }
        public class GetRespuestaParticipanteQueryHandler : IRequestHandler<GetRespuestaParticipanteQuery, IList<PreguntaRespuestaVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetRespuestaParticipanteQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IList<PreguntaRespuestaVM>> Handle(GetRespuestaParticipanteQuery request, CancellationToken cancellationToken)
            {               
                var LISTA = (from pregunta in _context.TPreguntas
                              join respuesta in _context.TRespuestaParticipante on pregunta.CodPregunta equals respuesta.CodPregunta into lista
                             from list in lista.DefaultIfEmpty()
                             where (list.CodCurso == request.codCurso && list.CodPersona == request.codPersona)
                             select new PreguntaRespuestaVM
                             {
                                 CodCurso = list.CodCurso,
                                 CodPersona = list.CodPersona,
                                 CodPregunta = list.CodPregunta,
                                 Pregunta= pregunta.Descripcion,
                                 Tipo = pregunta.Tipo,
                                 Respuesta = list.Respuesta,
                                 Nota = list.Nota,
                                 Puntaje= pregunta.Puntaje
                             }).ToList();//.OrderBy(i => i.CodPregunta)
                foreach (var item in LISTA) {
                    if (!item.Tipo.Equals("3")) {
                        List<int> respuestas = string.IsNullOrEmpty(item.Respuesta) ? new List<int>() : item.Respuesta.Split(";").ToList().ConvertAll(int.Parse); ;                       
                        if (respuestas.Count == 0) item.Respuesta = "Sin Respuesta";
                        //if (respuestas.Count == 1) {

                        //    item.Respuesta = _context.TAlternativas.Where(a => a.CodAlternativa == Convert.ToInt32(respuestas.First())).Select(s => s.Descripcion).FirstOrDefault();
                        //}
                        else {
                            var repsc = _context.TAlternativas.Where(a => respuestas.Contains(a.CodAlternativa)).ToList();                          
                            if (repsc.Count == 1) item.Respuesta = repsc.First().Descripcion;
                            else
                            {
                                var respOrde = repsc.OrderBy(t => t.CodAlternativa).Select((t, i) => string.Concat((i+1), ". ", t.Descripcion)).ToList();
                                item.Respuesta = string.Join(", ", respOrde);
                            }
                        }
                    }
                }
                return LISTA;
            }
        }
    }
}
