using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Update.Dtos;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Update.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Update
{
    public class UpdateTemaCapacitacionCommand : IRequest<Unit>
    {
        public UpdateTemaCapacitacionRequestVM VM { get; set; }
        public class UpdateTemaCapacitacionCommandHandler : IRequestHandler<UpdateTemaCapacitacionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public UpdateTemaCapacitacionCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(UpdateTemaCapacitacionCommand request, CancellationToken cancellationToken)
            {
                var codTemaCapacitaJson = request.VM.codTemaCapacita;
                var busquedaTema = _context.TTemaCapacitacion.Include(i => i.TemaCapEspecifico).FirstOrDefault(i => i.CodTemaCapacita == codTemaCapacitaJson);

                busquedaTema.CodTemaCapacita = request.VM.codTemaCapacita;
                busquedaTema.CodTipoTema = request.VM.codTipoTema;
                busquedaTema.CodAreaCapacita = request.VM.codAreaCapacita;
                busquedaTema.Descripcion = request.VM.descripcion;
                busquedaTema.CompetenciaHs = request.VM.competenciaHs;
                busquedaTema.CodHha = request.VM.codHha;
                busquedaTema.Estado = true;

                IList<UpdateTemaCapacitacionRequestDto> respJson = new List<UpdateTemaCapacitacionRequestDto>();
                var temasCapJson = request.VM.temaCapEspecifico;
                if (temasCapJson.Count() > 0)
                {
                    var respBD = busquedaTema.TemaCapEspecifico.ToList();

                    var inter = temasCapJson.Select(x => x.codPeligro).Intersect(respBD.Select(x => x.CodPeligro)).ToList();
                    var right = temasCapJson.Select(x => x.codPeligro).Except(respBD.Select(x => x.CodPeligro)).ToList();
                    var left = respBD.Select(x => x.CodPeligro).Except(temasCapJson.Select(x => x.codPeligro)).ToList();

                    // ESTA EN AMBIOS TANTO EN LA BD Y JSON !! POR ELLO ACTUALIZA !!!
                    if (inter != null)
                    {
                        foreach (var codPeligro in inter)
                        {
                            var temaCapBD = respBD.FirstOrDefault(i => i.CodPeligro == codPeligro);
                            if (temaCapBD != null)
                            {                           
                                temaCapBD.Estado = true;
                                _context.TTemaCapEspecifico.Update(temaCapBD);
                            }
                        }
                    }

                    // ESTA EN EL JSON PERO NO EN LA BD ... POR LO TANDO AGREGA !!
                    if (right != null)
                    {
                        foreach (var CodPeligro in right)
                        {
                            var temaCapJson = temasCapJson.FirstOrDefault(i => i.codPeligro == CodPeligro);
                            if (temaCapJson != null)
                            {
                                var temaCapEntity = new TTemaCapEspecifico();

                                temaCapEntity.CodTemaCapacita = codTemaCapacitaJson;
                                temaCapEntity.CodPeligro = temaCapJson.codPeligro;
                                temaCapEntity.CodRiesgo = temaCapJson.codRiesgo;
                                temaCapEntity.TemaCapacitacion = busquedaTema;
                                temaCapEntity.Estado = true;
                                _context.TTemaCapEspecifico.Add(temaCapEntity);                                                                
                            }
                        }
                    }

                    // ESTA EN LA BD PERO NO EN EL JSON .... POR LO TANTO ELIMINA LOGICAMENTE !!
                    if (left != null)
                    {
                        foreach (var codPeligro in left)
                        {
                            var temaCapBD = respBD.FirstOrDefault(i => i.CodPeligro == codPeligro && i.Estado);
                            if (temaCapBD != null)
                            {
                                temaCapBD.Estado = false;
                                _context.TTemaCapEspecifico.Update(temaCapBD);
                            }
                        }
                    }
                }
                _context.TTemaCapacitacion.Update(busquedaTema);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}