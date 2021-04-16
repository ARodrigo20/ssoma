using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Participantes.Command.Update.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Participantes.Command.Update
{
    public class UpdateParticipantesCommand : IRequest<Unit>
    {
        public UpdateParticipantesAllVM VM { get; set; }
        public class UpdateParticipantesCommandHandler : IRequestHandler<UpdateParticipantesCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public UpdateParticipantesCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(UpdateParticipantesCommand request, CancellationToken cancellationToken)
            {
                var modelVM = request.VM.data;

                foreach (var item in modelVM)
                {
                    var curso = _context.TCurso.Include(i => i.Participantes).FirstOrDefault(i => i.CodCurso == item.codTemaCapacita);

                    if (curso != null)
                    {
                        var respJson = item.participantes;
                        foreach (var it in respJson)
                        {
                            var respBD = curso.Participantes.ToList();

                            var inter = respJson.Select(x => x.codPersona).Intersect(respBD.Select(x => x.CodPersona)).ToList();
                            var right = respJson.Select(x => x.codPersona).Except(respBD.Select(x => x.CodPersona)).ToList();
                            var left = respBD.Select(x => x.CodPersona).Except(respJson.Select(x => x.codPersona)).ToList();
                            bool Eval = it.nota > 0;
                            if (inter != null)
                            {
                                foreach (var vals in inter)
                                {
                                    var partDB = respBD.FirstOrDefault(i => i.CodPersona == vals);
                                    var partJson = respJson.FirstOrDefault(i => i.codPersona == vals);
                                    partDB.Nota = partJson.nota;
                                    partDB.Tipo = partJson.tipo;
                                    partDB.Evaluado = Eval;
                                    _context.TParticipantes.Update(partDB);
                                }
                            }

                            if (right != null)
                            {
                                foreach (var vals in right)
                                {
                                    var partJson = respJson.FirstOrDefault(i => i.codPersona == vals);
                                    if (partJson != null)
                                    {
                                        var partEntity = new TParticipantes();
                                        partEntity.Nota = partJson.nota;
                                        partEntity.Tipo = partJson.tipo;
                                        partEntity.Evaluado = Eval;
                                        partEntity.Estado = true;
                                        _context.TParticipantes.Add(partEntity);
                                    }
                                }
                            }

                            if (left != null)
                            {
                                foreach (var vals in left)
                                {
                                    var partDB = respBD.FirstOrDefault(i => i.CodPersona == vals && i.Estado);
                                    if (partDB != null)
                                    {
                                        partDB.Estado = false;
                                        _context.TParticipantes.Update(partDB);
                                    }
                                }
                            }                            
                        }
                    }                    
                }
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}




