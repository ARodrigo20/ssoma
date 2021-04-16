using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Preguntas.Command.Update.DTOs;
using Hsec.Application.Capacitaciones.Preguntas.Command.Update.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Preguntas.Command.Update
{
    public class UpdatePreguntaCommand : IRequest<Unit>
    {
        public UpdatePreguntaVM VM { get; set; }
        public class CreatePlanTemaCommandHandler : IRequestHandler<UpdatePreguntaCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreatePlanTemaCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(UpdatePreguntaCommand request, CancellationToken cancellationToken)
            {
                var codCurso = request.VM.codCurso;
                var verifCursoActivo = _context.TCurso.Any(i => i.CodCurso == codCurso && i.Estado);

                if (verifCursoActivo)
                {
                    var pregListAllBD = _context.TPreguntas.Include(i => i.Alternativas).Where(i => i.CodCurso == codCurso);

                    var pregListJson = request.VM.data;
                    var pregListBD = pregListAllBD.Select(i => i.CodPregunta).ToList();

                    var interPreg = pregListJson.Select(x => x.codPregunta).Intersect(pregListBD).ToList(); // Existentes, actualizar
                    var rightPreg = pregListJson.Select(x => x.codPregunta).Except(pregListBD).ToList(); // Nuevas alternativas, agregar
                    var leftPreg = pregListBD.Except(pregListJson.Select(x => x.codPregunta)).ToList(); // Eliminadas

                    if (interPreg.Count > 0)
                    { // ACTUALIZAMOS LAS PREGUNTAS !!
                        foreach (var codPregunta in interPreg)
                        {
                            var preguntaJson = pregListJson.FirstOrDefault(i => i.codPregunta == codPregunta);
                            var preguntaBD = pregListAllBD.FirstOrDefault(i => i.CodPregunta == codPregunta);

                            preguntaBD.Descripcion = preguntaJson.descripcion;
                            preguntaBD.Tipo = preguntaJson.tipo;
                            preguntaBD.Puntaje = preguntaJson.puntaje;
                            preguntaBD.Estado = true;
                            var respuesta = "";

                            var alterJSON = preguntaJson.alternativas;
                            var alterBD = preguntaBD.Alternativas;

                            var inter = alterJSON.Select(x => x.codAlternativa).Intersect(alterBD.Select(x => x.CodAlternativa)).ToList(); // Existentes, actualizar
                            var right = alterJSON.Select(x => x.codAlternativa).Except(alterBD.Select(x => x.CodAlternativa)).ToList(); // Nuevas alternativas, agregar
                            var left = alterBD.Select(x => x.CodAlternativa).Except(alterJSON.Select(x => x.codAlternativa)).ToList(); // Eliminadas

                            List<string> codsResp = string.IsNullOrEmpty(preguntaJson.respuesta) ? new List<string>() : preguntaJson.respuesta.Split(";").ToList();
                            IList<int> listCodRespAlt = codsResp.Select(i => int.Parse(i)).ToList();
                            // Todos los codAlternativa de la respuesta .. tiene que coincidir con los codAlternativa de las alternativas !! 
                            var listCodsVal = listCodRespAlt.Intersect(alterJSON.Select(i => i.codAlternativa)).ToList();
                            bool validador = false;
                            if (listCodsVal.Count == listCodRespAlt.Count)
                            {
                                validador = true;
                            }

                            if (validador && ((listCodsVal.Count > 0 && alterJSON.Count >= listCodsVal.Count) || (listCodsVal.Count == 0 && alterJSON.Count == 0))) // Si no hay un codAlternativa de las respuesta que coincida con alguno de alternativas (Respuesta Invalida)!!
                            {
                                if (inter.Count > 0) //Actualizar Alternativa !!
                                {
                                    foreach (var CodAlter in inter)
                                    {
                                        var altBD = alterBD.FirstOrDefault(i => i.CodAlternativa == CodAlter);
                                        if (altBD != null)
                                        {
                                            var altJson = alterJSON.FirstOrDefault(i => i.codAlternativa == CodAlter);
                                            altBD.Descripcion = altJson.descripcion;
                                            altBD.Estado = true;
                                            if (listCodsVal.Contains(altBD.CodAlternativa))
                                            {
                                                if (!string.IsNullOrEmpty(respuesta))
                                                {
                                                    respuesta = respuesta + ";" + altBD.CodAlternativa.ToString();
                                                }
                                                else
                                                {
                                                    respuesta = altBD.CodAlternativa.ToString();
                                                }
                                            }
                                            _context.TAlternativas.Update(altBD);
                                            await _context.SaveChangesAsync(cancellationToken);
                                        }
                                    }
                                }

                                if (right.Count > 0) //Agregar Alternativa !!
                                {
                                    //var codAlterMax = _context.TAlternativas.Max(i => i.CodAlternativa);
                                    foreach (var CodAlter in right) //codAlter -1,-2 ..etc
                                    {
                                        //var listNeg = listCodsVal.Where(i => i < 0).ToList();
                                        if (CodAlter < 0)
                                        {
                                            var altJsonDesc = alterJSON.FirstOrDefault(i => i.codAlternativa == CodAlter).descripcion;
                                            if (altJsonDesc != null)
                                            {
                                                var alternEntity = new TAlternativas();
                                                alternEntity.CodPregunta = codPregunta;
                                                alternEntity.Descripcion = altJsonDesc;
                                                alternEntity.Estado = true;
                                                alternEntity.Preguntas = preguntaBD;
                                                _context.TAlternativas.Add(alternEntity);
                                                await _context.SaveChangesAsync(cancellationToken);

                                                var codAlterNew = alternEntity.CodAlternativa;

                                                if (listCodsVal.Contains(CodAlter))
                                                {
                                                    if (!string.IsNullOrEmpty(respuesta))
                                                    {
                                                        respuesta = respuesta + ";" + codAlterNew.ToString();
                                                    }
                                                    else
                                                    {
                                                        respuesta = codAlterNew.ToString();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                throw new ExceptionGeneral("Para poder agregar Alternativas en el codPregunta = " + codPregunta + "!! debe colocar numeros Negativos !! tanto en la respuesta como en las alternativas!!");
                                            }
                                        }
                                    }
                                }

                                if (left.Count > 0) //Eliminar logicamente Alternativa !!
                                {
                                    foreach (var CodAlter in left)
                                    {
                                        var altBD = alterBD.FirstOrDefault(i => i.CodAlternativa == CodAlter && i.Estado);
                                        if (altBD != null)
                                        {
                                            altBD.Estado = false;
                                            _context.TAlternativas.Update(altBD);
                                            await _context.SaveChangesAsync(cancellationToken);
                                        }
                                    }
                                }
                                preguntaBD.Respuesta = respuesta;
                                _context.TPreguntas.Update(preguntaBD);
                                await _context.SaveChangesAsync(cancellationToken);
                            }

                            else
                            {
                                throw new ExceptionGeneral("Las respuesta(s) del codPregunta: " + codPregunta + " no coincide con alguna de las alternativas !! recuerde colocar por lo menos una respuesta valida !!");
                            }
                        }//Aqui termina el Foreach
                    }

                    if (rightPreg.Count > 0)
                    { // AGREGAMOS LAS PREGUNTAS !!
                        foreach (var codPregunta in rightPreg)
                        {
                            if (codPregunta < 0) {
                                var preguntaJson = pregListJson.FirstOrDefault(i => i.codPregunta == codPregunta);
                                var alterJson = preguntaJson.alternativas;

                                List<string> codsResp = string.IsNullOrEmpty(preguntaJson.respuesta) ?new List<string>():preguntaJson.respuesta.Split(";").ToList();
                                IList<int> listCodRespAlt = codsResp.Select(i => int.Parse(i)).ToList();
                                // Todos los codAlternativa de la respuesta .. tiene que coincidir con los codAlternativa de las alternativas !! 
                                var listCodsVal = listCodRespAlt.Intersect(alterJson.Select(i => i.codAlternativa)).ToList();
                                bool validador = false;
                                if (listCodsVal.Count == listCodRespAlt.Count)
                                {
                                    validador = true;
                                }

                                if (validador && ((listCodsVal.Count > 0 && alterJson.Count >= listCodsVal.Count) || (listCodsVal.Count == 0 && alterJson.Count == 0))) // Si no hay un codAlternativa de las respuesta que coincida con alguno de alternativas (Respuesta Invalida)!!
                                {
                                    TPreguntas preguntasEntityNew = new TPreguntas();
                                    preguntasEntityNew.CodCurso = codCurso;
                                    preguntasEntityNew.Descripcion = preguntaJson.descripcion;
                                    preguntasEntityNew.Tipo = preguntaJson.tipo;
                                    preguntasEntityNew.Puntaje = preguntaJson.puntaje;

                                    var alterJsonVerifMenorCero = alterJson.Count>0? alterJson.Any(i => i.codAlternativa < 0):true;
                                    //var preguntaBD = pregListAllBD.Any(i => i.CodPregunta != codPregunta && codPregunta < 0);

                                    //var alterBD = preguntaBD.Alternativas;
                                    //var inter = alterJson.Select(x => x.codAlternativa).Intersect(alterBD.Select(x => x.CodAlternativa)).ToList(); // Existentes, actualizar

                                    if (alterJsonVerifMenorCero && codPregunta < 0) {
                                        _context.TPreguntas.Add(preguntasEntityNew);
                                        await _context.SaveChangesAsync(cancellationToken);

                                        var respuesta = "";

                                        TAlternativas alter;
                                        foreach (var alt in alterJson)
                                        {
                                            //var listNeg = listCodsVal.Where(i => i < 0).ToList();

                                            var altJsonDesc = alt.descripcion;

                                            if (altJsonDesc != null)
                                            {
                                                alter = new TAlternativas();
                                                alter.CodPregunta = codPregunta;
                                                alter.Descripcion = alt.descripcion;
                                                alter.Estado = true;
                                                alter.Preguntas = preguntasEntityNew;
                                                _context.TAlternativas.Add(alter);
                                                await _context.SaveChangesAsync(cancellationToken);

                                                var codAlterNew = alter.CodAlternativa;

                                                if (listCodsVal.Contains(alt.codAlternativa))
                                                {
                                                    if (!string.IsNullOrEmpty(respuesta))
                                                    {
                                                        respuesta = respuesta + ";" + codAlterNew.ToString();
                                                    }
                                                    else
                                                    {
                                                        respuesta = codAlterNew.ToString();
                                                    }
                                                }
                                            }
                                        }
                                        if (alterJson.Count > 0) {
                                            preguntasEntityNew.Respuesta = respuesta;
                                            _context.TPreguntas.Update(preguntasEntityNew);
                                            await _context.SaveChangesAsync(cancellationToken);
                                        }
                                    }
                                    else
                                    {
                                        throw new ExceptionGeneral("Para poder agregar Alternativas en el codPregunta = " + codPregunta + "!! debe colocar numeros Negativos !! tanto en la respuesta como en las alternativas!!");
                                    }
                                }
                            }
                            else
                            {
                                throw new ExceptionGeneral("Pregunta Registrada pero con Error !! en el codPregunta: "+ codPregunta + "!! para poder guardar nueva Pregunta !! escriba el codPregunta Negativo !!");
                            }
                        }
                    }

                    if (leftPreg.Count > 0)
                    { // ELIMINAMOS LAS PREGUNTAS DE MANERA LOGICA DE LA BD !!
                        foreach (var codPregunta in leftPreg) {
                            var preguntaBD = pregListAllBD.FirstOrDefault(i => i.CodPregunta == codPregunta);
                            preguntaBD.Estado = false;
                            var altPreg = preguntaBD.Alternativas;
                            foreach (var alt in altPreg)
                            {
                                alt.Estado = false;
                                _context.TAlternativas.Update(alt);
                            }
                            _context.TPreguntas.Update(preguntaBD);
                            await _context.SaveChangesAsync(cancellationToken);
                        }
                    }
                }

                else {
                    throw new ExceptionGeneral("El curso codCurso: " + codCurso + " No se Encuentra Registrado !!");
                }
                return Unit.Value;
            }
        }
    }
}