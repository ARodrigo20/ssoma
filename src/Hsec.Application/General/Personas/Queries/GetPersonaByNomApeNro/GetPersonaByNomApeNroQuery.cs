using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Personas.Queries.GetPersonaByNomApeNro
{

    public class GetPersonaByNomApeNroQuery : IRequest<PersonaByVM>
    {
        public string text { get; set; }

        public class GetPersonaByNomApeNroQueryHandler : IRequestHandler<GetPersonaByNomApeNroQuery, PersonaByVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public GetPersonaByNomApeNroQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = httpContext;
            }

            public async Task<PersonaByVM> Handle(GetPersonaByNomApeNroQuery request, CancellationToken cancellationToken)
            {
                //var codPersona = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                //var personatoke= _context.TPersona.Find(codPersona);
                string query = request.text.Replace(" ", "");
                string Tipopersona = "1";
                if (query.Contains("_contrata")) {
                    Tipopersona = "2";
                    query = query.Replace("_contrata", "");
                }
                

                var persona = _context.TPersona
                   .Where(p => /*p.Estado == true &&*/
                               (p.NroDocumento.Contains(query)
                               || (p.ApellidoPaterno + p.ApellidoMaterno + p.Nombres.Replace(" ", "")).Contains(query)
                               || (p.ApellidoPaterno + p.Nombres.Replace(" ", "")).Contains(query)
                               || (p.Nombres.Replace(" ", "") + p.ApellidoPaterno + p.ApellidoMaterno).Contains(query)
                               ) && ((Tipopersona == "2") ? p.CodTipoPersona == "2" : p.CodTipoPersona == "1" || p.CodTipoPersona == "2" )
                   )
                   .OrderBy(t => t.NroDocumento)
                   .ThenBy(t => t.ApellidoPaterno)
                   .ThenBy(t => t.ApellidoMaterno)
                   .ThenBy(t => t.Nombres)
                   .FirstOrDefault();

                PersonaByVM perVM = _mapper.Map<PersonaByVM>(persona);
                return perVM;
            }
        }

        
    }
}