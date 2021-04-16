using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Personas.Queries.GetPersonaByNomApeNroAll
{

    public class GetPersonaByNomApeNroAllQuery : IRequest<ListPersonasVM>
    {
        public string text { get; set; }

        public class GetPersonaByNomApeNroQueryHandler : IRequestHandler<GetPersonaByNomApeNroAllQuery, ListPersonasVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPersonaByNomApeNroQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ListPersonasVM> Handle(GetPersonaByNomApeNroAllQuery request, CancellationToken cancellationToken)
            {
                //string query = request.text.Trim(' ');
                string query = request.text.Replace(" ", "");
                ListPersonasVM vm = new ListPersonasVM();

                vm.data = _context.TPersona
                    .Where(p => p.Estado == true &&
                                ( p.NroDocumento.Contains(query)
                                || (p.ApellidoPaterno + p.ApellidoMaterno + p.Nombres.Replace(" ", "")).Contains(query)
                                || (p.ApellidoPaterno + p.Nombres.Replace(" ", "")).Contains(query)
                                || (p.Nombres.Replace(" ", "") + p.ApellidoPaterno + p.ApellidoMaterno).Contains(query)
                                )
                    )
                    .OrderBy(t => t.NroDocumento)
                    .ThenBy(t => t.ApellidoPaterno)
                    .ThenBy(t => t.ApellidoMaterno)
                    .ThenBy(t => t.Nombres)
                    .Take(10)
                    .ProjectTo<PersonaDto>(_mapper.ConfigurationProvider)
                    .ToList();

                return vm;
            }
        }

        
    }
}