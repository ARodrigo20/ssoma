using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.AnalisisCausas.Commands.CreateAnalisisCausa
{
    public partial class CreateAnalisisCausaCommand : IRequest<string>
    {
        public string CodPadre { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }

        public class CreateAnalisisCausaCommandHandler : IRequestHandler<CreateAnalisisCausaCommand, string>
        {
            private readonly IApplicationDbContext _context;

            public CreateAnalisisCausaCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(CreateAnalisisCausaCommand request, CancellationToken cancellationToken)
            {
                var entity = new TAnalisisCausa();
                var maxCod = _context.TAnalisisCausa.Where(a => a.Nivel == request.Nivel).Max(ac => ac.CodAnalisis);
                if (maxCod == null) maxCod = "001";
                else
                {
                    string patron = @"(?:- *)?\d+(?:\.\d+)?";
                    Regex regex = new Regex(patron);
                    string Codint = regex.Match(maxCod).ToString();
                    string CodStr = maxCod.Replace(Codint, "");
                    int cod = int.Parse(Codint) + 1;
                    maxCod = CodStr+cod.ToString("D"+ Codint.Length);
                }
                entity.CodAnalisis = maxCod;
                entity.CodPadre = request.CodPadre;
                entity.Descripcion = request.Descripcion;
                entity.Nivel = request.Nivel;

                _context.TAnalisisCausa.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return maxCod;
            }
        }
    }
}
    

