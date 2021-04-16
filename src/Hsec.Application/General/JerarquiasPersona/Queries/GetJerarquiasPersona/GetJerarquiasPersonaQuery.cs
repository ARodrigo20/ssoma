using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Hsec.Application.Common.Interfaces;

namespace Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiaPersona
{
    public class GetJerarquiasPersonaQuery : IRequest<JerarquiasPersonaVM>
    {
        public string CodPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FecInicio { get; set; }
        public string FecFin { get; set; }
        public string Asignacion { get; set; }
        public string TipoAsig { get; set; }
        public int CodPosicion { get; set; }
        public class GetJerarquiasPersonaQueryHandler : IRequestHandler<GetJerarquiasPersonaQuery, JerarquiasPersonaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            

            public GetJerarquiasPersonaQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<JerarquiasPersonaVM> Handle(GetJerarquiasPersonaQuery request, CancellationToken cancellationToken)
            {
                //request cod posicion -> extrae TJerarquiaPersona segun el key de Jerarquias
                JerarquiasPersonaVM padre = new JerarquiasPersonaVM();        
                //var persona = await _context.TPersona.FindAsync(request.CodPersona);
                padre.Data = _context.TJerarquiaPersona.Join(_context.TPersona,jer => jer.CodPersona, per => per.CodPersona, (jer, per) => new { jer = jer  ,per = per })
                .Where  (tuple =>(tuple.jer.CodPosicion == request.CodPosicion && tuple.jer.CodTipoPersona==1))
                .Select (t => new JerarquiasPersonaNodeVM(t.per.CodPersona, t.per.Nombres, t.per.ApellidoPaterno, 
                t.per.ApellidoMaterno, t.jer.FechaInicio, t.jer.FechaFin, t.jer.PosicionPrimaria) )
                .ToList();

                //foreach (var item in ListQuery) {              
                //    padre.Data.Add(item);
                //}
                    padre.count = padre.Data.Count;
                    return padre;
            }
        }
    }
}
        




