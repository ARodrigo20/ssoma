using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Commands.CreatePersona;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create
{
    public class CreateCursoProgramacionCommand : IRequest<CodCursoDTO>
    {
        public CreateCursoProgramacionDto VM { get; set; }
        public class CreateCursoProgramacionCommandHandler : IRequestHandler<CreateCursoProgramacionCommand, CodCursoDTO>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            public CreateCursoProgramacionCommandHandler(IApplicationDbContext context, IMediator mediator)
            {
                this._context = context;
                this._mediator = mediator;
            }

            public async Task<CodCursoDTO> Handle(CreateCursoProgramacionCommand request, CancellationToken cancellationToken)
            {
                CodCursoDTO codVM = new CodCursoDTO();
                TCurso curso;
                var item = request.VM;
                string format = "ddd MMM dd yyyy HH:mm:ss 'GMT'K";
                DateTime dateIni;
                DateTime dateFin;
                bool validFormatFecIni = DateTime.TryParseExact(item.fechaInicio, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateIni);
                bool validFormatFecFin = DateTime.TryParseExact(item.fechaFin, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFin);
                var codCurso = "";
                if (validFormatFecIni && validFormatFecFin && dateFin > dateIni) {
                    var numDig = "00000000";
                    //var numReg = _context.TCurso.Count();

                    curso = new TCurso();
                    curso.CodTemaCapacita = item.codTemaCapacita;
                    curso.CodTipoTema = item.codTipoTema;
                    curso.CodAreaCapacita = item.codAreaCapacita;
                    //curso.CodHha = item.codHha;
                    curso.CodEmpCapacita = item.codEmpCapacita;
                    curso.PuntajeTotal = item.puntajeTotal;
                    curso.PorcAprobacion = item.porcAprobacion;
                    curso.RecurrenceID = item.recurrenceID;
                    curso.Enlace = item.enlace;
                    curso.Online = item.online;
                    curso.Duracion = item.duracion;
                    int temp2;

                    //var ultNum = _context.TCurso.Max(t => t.CodCurso);

                    //var ultNum = _context.TCurso.Select(x => x.CodCurso).ToList().Select(n => int.TryParse(n, out temp2) ? temp2 : 0).Max()+1;

                    //codCurso = numDig.Substring(0, numDig.Length - ultNum.ToString().Length) + ultNum;
                    codCurso = nextCod();

                    curso.CodCurso = codCurso;

                    curso.CodLugarCapacita = item.codLugarCapacita;
                    curso.CodSala = item.codSala;
                    curso.Capacidad = item.capacidad;
                    curso.Vigencia = item.vigencia;
                    curso.CodVigenciaCapacita = item.codVigenciaCapacita;

                    curso.FechaInicio = dateIni;
                    curso.FechaFin = dateFin;


                   
                    curso.Estado = true;

                    if (item.expositores.Count > 0) {
                        TExpositor expositor;
                        foreach (var it in item.expositores) {
                            expositor = new TExpositor();
                            expositor.CodPersona = it.codPersona;
                           // expositor.CodCurso = it.codTemaCapacita;
                            expositor.Curso = curso;
                            expositor.Estado = true;
                            expositor.Tipo = it.tipo;
                            if (!it.tipo && String.IsNullOrEmpty(it.codPersona))
                            {
                                //var expositorExtCodPersona = await _expoExt.RequestCodPersonaExpositor(it.personaExterna);
                                var expositorExtCodPersona = await _mediator.Send(new CreatePersonaExpositorCommand() { Sexo = it.personaExterna.sexo.ToString(), NroDocumento = it.personaExterna.nroDocumento, Nombres = it.personaExterna.nombres, ApellidoPaterno = it.personaExterna.apellidoPaterno, ApellidoMaterno = it.personaExterna.apellidoMaterno, Ocupacion = it.personaExterna.ocupacion, Empresa = it.personaExterna.empresa } );
                                expositor.CodPersona = expositorExtCodPersona;
                            }
                            curso.Expositores.Add(expositor);
                        }
                    }

                    if (item.participantes.Count > 0)
                    {
                        TParticipantes part;
                        foreach (var it in item.participantes)
                        {
                            part = new TParticipantes();
                            part.CodPersona = it.codPersona;
                            part.Curso = curso;
                            part.Estado = true;
                            part.Nota = it.nota;
                            part.Tipo = it.tipo;
                            curso.Participantes.Add(part);
                        }
                    }
                    _context.TCurso.Add(curso);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    Console.Write("FECHAS START Y END INVALIDAS ... Recuerde END > START");
                }

                codVM.codCurso = codCurso;
                return codVM;
            }

            public string nextCod()
            {
                var COD_CURSO_MAX = _context.TCurso.Max(t => t.CodCurso);
                if (COD_CURSO_MAX == null) COD_CURSO_MAX = "00000001";
                else
                {
                    string numberStr = COD_CURSO_MAX.Substring(0);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_CURSO_MAX = String.Format("{0,8:00000000}", max);
                }
                return COD_CURSO_MAX;
            }
        }
    }
}