using Hsec.Application.Common.Interfaces;
using Hsec.Application.Capacitaciones.Cursos_Acreditacion.Command.Create.VMs;
using Hsec.Domain.Entities.Capacitaciones;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Cursos_Acreditacion.Command.Create
{
    public class CreateCursoAcreditacionStikerCommand : IRequest<CreateCursoAcreditacionVM>
    {
        public CreateCursoAcreditacionRequestVM VM { get; set; }
        public class CreateCursoAcreditacionStikerCommandHandler : IRequestHandler<CreateCursoAcreditacionStikerCommand, CreateCursoAcreditacionVM>
        {
            private readonly IApplicationDbContext _context;

            public CreateCursoAcreditacionStikerCommandHandler(IApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<CreateCursoAcreditacionVM> Handle(CreateCursoAcreditacionStikerCommand request, CancellationToken cancellationToken)
            {
                var modeloVM = request.VM;
                CreateCursoAcreditacionVM vmResp = new CreateCursoAcreditacionVM();
                var valAcredCur = _context.TAcreditacionCurso.Any(i => i.CodCurso == modeloVM.codCurso && i.CodPersona == modeloVM.codPersona && i.Estado);
                if (valAcredCur)
                {
                    var TAcredCurso = _context.TAcreditacionCurso.FirstOrDefault(i => i.CodCurso == modeloVM.codCurso && i.CodPersona == modeloVM.codPersona && i.Estado);
                    TAcredCurso.CodCurso = modeloVM.codCurso;
                    TAcredCurso.CodPersona = modeloVM.codPersona;
                    TAcredCurso.CodStiker = modeloVM.codStiker;
                    TAcredCurso.Candado = modeloVM.candado;
                    TAcredCurso.FechaStiker = modeloVM.fechaStiker;
                    TAcredCurso.FechaTarjeta = modeloVM.fechaTarjeta; //error
                    _context.TAcreditacionCurso.Update(TAcredCurso);
                    await _context.SaveChangesAsync(cancellationToken);
                    vmResp.codigo = TAcredCurso.CodStiker;
                }
                else
                {
                    TAcreditacionCurso acredCurso = new TAcreditacionCurso();
                    var codsTarjetas = modeloVM.candado;
                    acredCurso.CodCurso = modeloVM.codCurso;
                    acredCurso.CodPersona = modeloVM.codPersona;
                    acredCurso.CodStiker = modeloVM.codStiker;
                    acredCurso.Candado = null;
                    acredCurso.FechaStiker = modeloVM.fechaStiker;
                    acredCurso.FechaTarjeta = null; //error
                    acredCurso.Estado = true;
                    _context.TAcreditacionCurso.Add(acredCurso);
                    await _context.SaveChangesAsync(cancellationToken);
                    vmResp.codigo = acredCurso.CodStiker;
                }
                return vmResp;
            }
        }
    }
}