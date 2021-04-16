using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command.VM;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command
{
    public class UpdateNotaPreguntaCommand : IRequest<PreguntasEvaluacionRespVM>
    {
        public PreguntaEvaluacionVM VM { get; set; }
        public class UpdateNotaPreguntaCommandHandler : IRequestHandler<UpdateNotaPreguntaCommand, PreguntasEvaluacionRespVM>
        {
            private readonly IApplicationDbContext _context;

            public UpdateNotaPreguntaCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<PreguntasEvaluacionRespVM> Handle(UpdateNotaPreguntaCommand request, CancellationToken cancellationToken)
            {
                var codCurso = request.VM.codCurso;
                var codPersona = request.VM.codPersona;

             //   var pregListAllBD = _context.TPreguntas.Where(i => i.CodCurso == codCurso && i.Estado);
                foreach (var item in request.VM.data)  // 
                {
                    var ParticiResp = _context.TRespuestaParticipante.FirstOrDefault(I => I.CodCurso == codCurso && I.CodPersona == codPersona && I.CodPregunta == item.codPregunta && I.Estado);
                    if (ParticiResp == null)
                    {
                        TRespuestaParticipante temp = new TRespuestaParticipante();
                        temp.CodCurso = codCurso;
                        temp.CodPersona = codPersona;
                        temp.CodPregunta = item.codPregunta;
                        temp.Nota = (decimal)(Math.Round(item.nota, 1));
                        //temp.Respuesta = item.respuesta;
                        _context.TRespuestaParticipante.Add(temp);
                    }
                    else
                    {
                        ParticiResp.Nota = (decimal)(Math.Round(item.nota, 1));
                        //ParticiResp.Respuesta = item.respuesta;
                        _context.TRespuestaParticipante.Update(ParticiResp);
                    }
                    await _context.SaveChangesAsync(cancellationToken);                   
                }
                var notaTotal = _context.TRespuestaParticipante.Where(r => r.CodCurso == codCurso && r.CodPersona == codPersona).Select(s => s.Nota).Sum();
                var part = _context.TParticipantes.FirstOrDefault(I => I.CodPersona == codPersona && I.CodCurso == codCurso);
                if (part != null)
                {
                    part.Nota = notaTotal;
                    part.Evaluado = true;
                    _context.TParticipantes.Update(part);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                PreguntasEvaluacionRespVM VMResp = new PreguntasEvaluacionRespVM();
                VMResp.puntajeTotal = notaTotal;
                VMResp.estadoEvaluacion = codPersona;
                return VMResp;
            }
        }
    }
}
