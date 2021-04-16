using Microsoft.Extensions.Configuration;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Preguntas.Command.Update;
using Hsec.Application.Capacitaciones.Preguntas.Command.Update.VMs;
using Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command.VM;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command
{
    public class PreguntasEvaluacionCommand : IRequest<PreguntasEvaluacionRespVM>
    {
        public PreguntasEvaluacionVM VM { get; set; }
        public class PreguntasEvaluacionCommandHandler : IRequestHandler<PreguntasEvaluacionCommand, PreguntasEvaluacionRespVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IConfiguration _config;

            public PreguntasEvaluacionCommandHandler(IApplicationDbContext context, IConfiguration configuration)
            {
                _context = context;
                _config = configuration;
            }

            public async Task<PreguntasEvaluacionRespVM> Handle(PreguntasEvaluacionCommand request, CancellationToken cancellationToken)
            {
                var codCurso = request.VM.codCurso;
                var codPersona = request.VM.codPersona;

                var verifCursoActivo = _context.TCurso.Any(i => i.CodCurso == codCurso && i.Estado);
                //var verifPartActivo = _context.TParticipantes.Any(I => I.CodPersona == codPersona && I.Estado);
                var part = _context.TParticipantes.FirstOrDefault(I => I.CodPersona == codPersona && I.CodCurso == codCurso && I.Estado);
                
                PreguntasEvaluacionRespVM VMResp = new PreguntasEvaluacionRespVM();

                if (verifCursoActivo && part != null)
                {
                    int intentos = Convert.ToInt32(_config["appSettings:IntentosEvaluacion"]);
                    if(part.Intentos >= intentos) throw new ExceptionGeneral("Solo se esta permitido "+ intentos + " intentos de evaluación, consulte con el expositor del curso");
                    var pregListAllBD = _context.TPreguntas.Include(i => i.Alternativas).Where(i => i.CodCurso == codCurso && i.Estado);
                    var pregListJson = request.VM.data;

                    var pregListBD = pregListAllBD.Select(i => i.CodPregunta).ToList();

                    var interPreg = pregListJson.Select(x => x.codPregunta).Intersect(pregListBD).ToList(); // Existentes, actualizar

                    if (interPreg.Count > 0)
                    {
                        double puntajeTotal = 0;
                        var curso = await _context.TCurso.FirstOrDefaultAsync(i => i.CodCurso == codCurso && i.Estado);

                        var porcentajeEval = curso.PorcAprobacion;
                        var puntTotal = curso.PuntajeTotal;
                        bool NotaPendiente = false; 

                        foreach (var codPregunta in interPreg)
                        {
                            var preguntaJson = pregListJson.FirstOrDefault(i => i.codPregunta == codPregunta);
                            var preguntaBD = pregListAllBD.FirstOrDefault(i => i.CodPregunta == codPregunta);
                            var NotaPregunta = 0.0;

                            if (preguntaJson.tipo.Equals("1")) // Seleccion Simple 
                            {
                                string[] codsResp = preguntaJson.respuesta.Split(";");
                                string listCodRespAlt = codsResp.Select(i => i).FirstOrDefault();

                                if (listCodRespAlt == preguntaBD.Respuesta)
                                {
                                    NotaPregunta = preguntaBD.Puntaje;                                   
                                }                               
                            }

                            else if (preguntaJson.tipo.Equals("2")) // Seleccion Multiple
                            {

                                string[] codsRespJson = preguntaJson.respuesta.Split(";");
                                string[] codsRespBD = preguntaBD.Respuesta.Split(";");

                                IList<string> listCodRespJson = codsRespJson.Select(i => i).ToList();
                                IList<string> listCodRespBD = codsRespBD.Select(i => i).ToList(); ;


                                var listCorrectas = listCodRespJson.Intersect(listCodRespBD).ToList(); // Correcta
                                var listInCorrectas = listCodRespJson.Except(listCodRespBD).ToList(); // Incorrecta

                                if (listCorrectas.Count() > 0)
                                {
                                    foreach (var item in listCorrectas)
                                    {                                       
                                        var data = pregListAllBD.ToList().FirstOrDefault(i => i.Respuesta.Split(";").ToList().Contains(item));
                                        if (data != null)
                                        {                                            
                                            NotaPregunta = NotaPregunta + (data.Puntaje / codsRespBD.Count());
                                        }
                                    }
                                }                               
                            }

                            else if(preguntaJson.tipo.Equals("3")) // Completar
                            {
                                if (!string.IsNullOrEmpty(preguntaBD.Respuesta))
                                {
                                    var Alter = preguntaBD.Alternativas.Where(i => i.Descripcion.Trim().ToLower().Contains(preguntaJson.respuesta.Trim().ToLower())).FirstOrDefault();
                                    if (Alter != null)
                                    {
                                        NotaPregunta = preguntaBD.Puntaje;
                                    }
                                }
                                else {
                                    NotaPendiente = true;
                                }
                            }

                            else
                            {
                                //throw new ExceptionGeneral("Porfavor Marcar una sola respuesta !! ya que es de tipo Seleccion Simple");
                                throw new ExceptionGeneral("Tipo de Pregunta Incorrecta !! Solo hay 3 Tipos !! seleccion simple (1), multiple (2) o llenar (3)");

                            }
                            // guardando respuesta de particiapnte
                            var ParticiResp = _context.TRespuestaParticipante.FirstOrDefault(I => I.CodCurso == codCurso && I.CodPersona == codPersona && I.CodPregunta == codPregunta && I.Estado);
                            if (ParticiResp == null)
                            {
                                TRespuestaParticipante temp = new TRespuestaParticipante();
                                temp.CodCurso = codCurso;
                                temp.CodPersona = codPersona;
                                temp.CodPregunta = codPregunta;
                                temp.Nota = (decimal)(Math.Round(NotaPregunta, 1));
                                temp.Respuesta = preguntaJson.respuesta;
                                _context.TRespuestaParticipante.Add(temp);
                            }
                            else {
                                ParticiResp.Nota = (decimal)(Math.Round(NotaPregunta, 1));
                                ParticiResp.Respuesta = preguntaJson.respuesta;
                                _context.TRespuestaParticipante.Update(ParticiResp);
                            }
                            await _context.SaveChangesAsync(cancellationToken);
                            puntajeTotal = puntajeTotal + NotaPregunta;
                        }

                        part.Nota = (decimal)(Math.Round(puntajeTotal, 1));
                        part.Evaluado = !NotaPendiente;
                        part.Intentos = part.Intentos + 1;
                        _context.TParticipantes.Update(part);
                        await _context.SaveChangesAsync(cancellationToken);

                        VMResp.nota = puntajeTotal;
                        VMResp.porcentajeEval = porcentajeEval;
                        VMResp.puntajeTotal = puntTotal;
                        VMResp.estadoEvaluacion = NotaPendiente ? "Pendiente" : ((puntajeTotal >= ((double)puntTotal) * (porcentajeEval / 100)) ? "Aprobado" : "Desaprobado");
                    }
                    else
                    {
                        //throw new ExceptionGeneral("El curso codCurso: " + codCurso + " No se Encuentra Registrado !!");
                        throw new ExceptionGeneral("El codPregunta que usted marco !! no esta registrado en el sistema !!");
                    }
                }
                else {
                    throw new ExceptionGeneral("El codCurso o codPersona que ud coloco no estan registrados en el sistema !!");
                }
                return VMResp;
            }
        }
    }
}