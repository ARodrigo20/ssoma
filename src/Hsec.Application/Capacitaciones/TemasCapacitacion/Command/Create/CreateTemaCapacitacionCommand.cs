using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Create.DTOs;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Create.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Create
{
    public class CreateTemaCapacitacionCommand : IRequest
    {
        public CreateTemaCapacitacionRequestVM CreateVM { get; set; }
        public class CreateTemaCapacitacionCommandHandler : IRequestHandler<CreateTemaCapacitacionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreateTemaCapacitacionCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(CreateTemaCapacitacionCommand request, CancellationToken cancellationToken)
            {
                var codTipoTema = request.CreateVM.codTipoTema;
                
                var numDig = "000";
                if (!String.IsNullOrEmpty(codTipoTema)) {
                    
                    var prefijo = "";
                    if (codTipoTema == "01") {
                        prefijo = "CE";
                    }

                    else if (codTipoTema == "02") {
                        prefijo = "OE";
                    }

                    else if (codTipoTema == "03") {
                        prefijo = "TE";
                    }

                    var numReg = _context.TTemaCapacitacion.Where(I => I.CodTemaCapacita.Substring(0, 2) == prefijo).Count();
                    if (!String.IsNullOrEmpty(prefijo)) {
                       
                        var ultNum = 1;
                        if (numReg > 0)
                        {
                            ultNum = int.Parse(_context.TTemaCapacitacion.Where(I=>I.CodTemaCapacita.Substring(0,2) == prefijo).Max(i => i.CodTemaCapacita.Substring(2,i.CodTemaCapacita.Length))) + 1;
                        }
                        var codTemaCapacita = prefijo + numDig.Substring(0, numDig.Length - ultNum.ToString().Length) + ultNum;
                                               
                        TTemaCapacitacion tema = new TTemaCapacitacion();

                        var desc = request.CreateVM.descripcion.Trim();
                        var verDesc = _context.TTemaCapacitacion.Any(i => i.Estado && i.Descripcion.Trim().Equals(desc));

                        if (!verDesc)
                        {
                            tema.CodTemaCapacita = codTemaCapacita;
                            tema.CodTipoTema = codTipoTema;
                            tema.CodAreaCapacita = request.CreateVM.codAreaCapacita;
                            tema.Descripcion = request.CreateVM.descripcion;
                            tema.CompetenciaHs = request.CreateVM.competenciaHs;
                            tema.CodHha = request.CreateVM.codHha;
                            tema.Estado = true;

                            var temCapListVM = request.CreateVM.temaCapEspecifico;

                            if (temCapListVM.Count() > 0)
                            {
                                TTemaCapEspecifico tempcap;
                                foreach (var item in temCapListVM)
                                {
                                    tempcap = new TTemaCapEspecifico();
                                    tempcap.CodTemaCapacita = tema.CodTemaCapacita;
                                    tempcap.CodPeligro = item.codPeligro;
                                    tempcap.CodRiesgo = item.codRiesgo;
                                    tempcap.Estado = true;
                                    tema.TemaCapEspecifico.Add(tempcap);
                                }
                            }
                            _context.TTemaCapacitacion.Add(tema);
                            await _context.SaveChangesAsync(cancellationToken);
                        }

                        else {
                            throw new ExceptionGeneral("Nombre del Tema ya existente !!");
                        }
                    }                    
                }
                return Unit.Value;
            }
        }
    }
}