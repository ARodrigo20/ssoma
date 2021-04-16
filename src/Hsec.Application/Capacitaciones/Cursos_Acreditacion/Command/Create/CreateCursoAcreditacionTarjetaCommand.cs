using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Cursos_Acreditacion.Command.Create.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Cursos_Acreditacion.Command.Create
{
    public class CreateCursoAcreditacionTarjetaCommand : IRequest<CreateCursoAcreditacionVM>
    {
        public CreateCursoAcreditacionRequestVM VM { get; set; }
        public class CreateCursoAcreditacionTarjetaCommandHandler : IRequestHandler<CreateCursoAcreditacionTarjetaCommand, CreateCursoAcreditacionVM>
        {
            private readonly IApplicationDbContext _context;

            public CreateCursoAcreditacionTarjetaCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<CreateCursoAcreditacionVM> Handle(CreateCursoAcreditacionTarjetaCommand request, CancellationToken cancellationToken)
            {
                var modeloVM = request.VM;
                CreateCursoAcreditacionVM vmResp = new CreateCursoAcreditacionVM();
                var valAcredCur = _context.TAcreditacionCurso.Any(i => i.CodCurso == modeloVM.codCurso && i.CodPersona == modeloVM.codPersona && i.Estado);
                if (valAcredCur)
                {
                    var TAcredCurso = _context.TAcreditacionCurso.FirstOrDefault(i => i.CodCurso == modeloVM.codCurso && i.CodPersona == modeloVM.codPersona && i.Estado);
                    var codsTarjetas = modeloVM.candado;
                    TAcredCurso.CodCurso = modeloVM.codCurso;
                    TAcredCurso.CodPersona = modeloVM.codPersona;
                    TAcredCurso.CodStiker = modeloVM.codStiker;
                    TAcredCurso.Candado = codsTarjetas;
                    TAcredCurso.FechaStiker = modeloVM.fechaStiker;
                    TAcredCurso.FechaTarjeta = modeloVM.fechaTarjeta;
                     _context.TAcreditacionCurso.Update(TAcredCurso);
                    await _context.SaveChangesAsync(cancellationToken);
                    vmResp.codigo = TAcredCurso.Candado;
                }
                else
                {
                    TAcreditacionCurso acredCurso = new TAcreditacionCurso();

                    var codsTarjetas = modeloVM.candado;
                    acredCurso.CodCurso = modeloVM.codCurso;
                    acredCurso.CodPersona = modeloVM.codPersona;
                    acredCurso.CodStiker = null;
                    acredCurso.Candado = codsTarjetas;
                    acredCurso.FechaStiker = null;
                    acredCurso.FechaTarjeta = modeloVM.fechaTarjeta;
                    acredCurso.Estado = true;
                    _context.TAcreditacionCurso.Add(acredCurso);
                    await _context.SaveChangesAsync(cancellationToken);
                    vmResp.codigo = acredCurso.Candado;
                }
                return vmResp;
            }
        }
    }
}