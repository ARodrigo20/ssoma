using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Maestro.Queries.GetMaestroDescripcion
{
    public class GetMaestroDescripcionQuery : IRequest<ICollection<string>>
    {
        public ICollection<string> data { get; set; }
        public class GetMaestroDataQueryHandler : IRequestHandler<GetMaestroDescripcionQuery, ICollection<string>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetMaestroDataQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

               
            }

            public async Task<ICollection<string>> Handle(GetMaestroDescripcionQuery request, CancellationToken cancellationToken)
            {
                var ResponseList = new List<string>();
                var list = request.data;
                foreach(var item in list)
                {
                    var items = item.Split('-');
                    var nameTable = items[0];
                    var codigoTable = items[1];

                    if (nameTable.Equals("Personas"))
                    {

                    }
                    var res = _context.TMaestro.Where(t => t.CodTabla.Equals(nameTable) && t.CodTipo.Equals(codigoTable)).Select(t => t.Descripcion).First();

                    ResponseList.Add(res);
                }
                return ResponseList;
            }

        }
    }
}
