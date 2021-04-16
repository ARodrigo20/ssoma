using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Inscripcion_Personas.Command.Create
{
    public class CreateInscripcionCommand : IRequest<Unit>
    {
        public string codPersona { get; set; }
        public string codCurso { get; set; }

        public class CreateInscripcionCommandHandler : IRequestHandler<CreateInscripcionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreateInscripcionCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(CreateInscripcionCommand request, CancellationToken cancellationToken)
            {
                //var codPersona = request.codPersona;
                //var codCurso = request.codCurso;
                var dateActual = DateTime.Now;
                var verificadorCurso = _context.TCurso.FirstOrDefault(i => i.CodCurso == request.codCurso && i.Estado);
                TParticipantes participante;

                var inscrito = _context.TCurso.Join(_context.TParticipantes, jer => jer.CodCurso, jper => jper.CodCurso, (jer, jper) => new { jer = jer, jper = jper })
                  .Any(p => p.jper.Estado && p.jer.Estado && p.jer.CodTemaCapacita.Equals(verificadorCurso.CodTemaCapacita) && p.jper.CodPersona.Equals(request.codPersona)
                  && ((p.jer.FechaInicio.Date >= dateActual.Date) || (p.jer.FechaInicio.Date <= dateActual.Date && dateActual.Date <= p.jer.FechaFin.Date)));
                  
                //var inscrito2 = (from curso in _context.TCurso
                //                join part in _context.TParticipantes on curso.CodCurso equals part.CodCurso
                //                where (curso.CodTemaCapacita.Equals(verificadorCurso.CodTemaCapacita) && part.CodPersona.Equals(request.codPersona) && curso.Estado && part.Estado &&
                //                ((curso.FechaInicio.Date >= dateActual.Date) || (curso.FechaInicio.Date <= dateActual.Date && dateActual.Date <= curso.FechaFin.Date))) select part).FirstOrDefault();
                // si existe dicho curso en la BD !!
                if (!inscrito)
                {
                    var verif = _context.TParticipantes.Where(i => i.CodPersona == request.codPersona && i.CodCurso == request.codCurso).FirstOrDefault();
                    // si la persona es participante !!
                    //if (verif!= null)
                    //{
                    //var verif = verificadorPersonaPart.FirstOrDefault(i => i.CodCurso == codCurso);
                    // si existe dicho participante matriculado en dicho curso !!
                    if (verif != null)
                    {
                        // si el participante estaba anteriormente matriculao en dicho curso !! se reactiva su Estado de Registro !!
                        if (!verif.Estado)
                        {
                            verif.Estado = true;
                            _context.TParticipantes.Update(verif);
                        }
                        else
                        {
                            throw new ExceptionGeneral("EL PARTICIPANTE YA SE ENCUENTRA REGISTRADO");
                        }

                    }
                    // si NO existe dicho participante matriculado en dicho curso... SE Procede a matricular !! el participante
                    // dentro de un curso existente!!
                    else
                    {
                        participante = new TParticipantes();
                        participante.CodCurso = verificadorCurso.CodCurso;
                        participante.CodPersona = request.codPersona;
                        participante.Curso = verificadorCurso;
                        participante.Nota = null;
                        participante.Tipo = true;
                        _context.TParticipantes.Add(participante);
                    }
                    //}
                }

                else
                {
                    throw new ExceptionGeneral("EL PARTICIPANTE YA SE ENCUENTRA REGISTRADO EN EL CURSO");// OTRO CURSO DEL MISMO TEMA
                }

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}