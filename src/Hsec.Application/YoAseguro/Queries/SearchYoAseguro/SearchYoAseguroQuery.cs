using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Hsec.Application.YoAseguro.Queries.SearchYoAseguro
{
    public class SearchYoAseguroQuery : IRequest<SearchYoAseguroVM>
    {
        public string CodPosGerencia { get; set; }
        public string Reunion { get; set; }
        public string CodYoAseguro { get; set; }
        public string TituloReunion { get; set; }

        public class SearchYoAseguroQueryHandler : IRequestHandler<SearchYoAseguroQuery, SearchYoAseguroVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;

            public SearchYoAseguroQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor HttpContext)
            {
                _context = context;
                _mapper = mapper;
                _httpContext = HttpContext;
            }

            public async Task<SearchYoAseguroVM> Handle(SearchYoAseguroQuery request, CancellationToken cancellationToken)
            {
                SearchYoAseguroVM VM = new SearchYoAseguroVM();
                string codPosicion = request.CodPosGerencia;

                var userToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var rolToken = _httpContext.HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                VM.data = _context.TYoAseguro
               .Where(t => t.Estado == true
                   && t.CodPosGerencia.Equals(request.CodPosGerencia)
                    && t.Reunion.Equals(request.Reunion)
                    && (t.CreadoPor.Contains(userToken) || rolToken != "6")
                    )
               .ProjectTo<SearchYoAseguroDto>(_mapper.ConfigurationProvider)
               .ToList();

                VM.size = VM.data.Count;

                foreach (var row in VM.data)
                { 
                    row.Editable = row.UsuCreacion == userToken || (new string[] { "1", "4" }).Contains(rolToken);
                }


                    //if (codPosicion == "-1")
                    //{
                    //    VM.data = _context.TYoAseguro
                    //   .Where(t => t.Estado == true
                    //       && (string.IsNullOrEmpty(request.CodYoAseguro) || t.CodYoAseguro.Equals(request.CodYoAseguro))
                    //       && (string.IsNullOrEmpty(request.TituloReunion) || t.TituloReunion.Contains(request.TituloReunion)))
                    //   .ProjectTo<SearchYoAseguroDto>(_mapper.ConfigurationProvider)
                    //   .ToList();

                    //    VM.size = VM.data.Count;
                    //} else
                    //{
                    //    VM.data = _context.TYoAseguro
                    //   .Where(t => t.Estado == true
                    //       && (t.CodPosGerencia.Equals(request.CodPosGerencia))
                    //       && (t.Reunion.Equals(request.Reunion))
                    //       && (string.IsNullOrEmpty(request.CodYoAseguro) || t.CodYoAseguro.Equals(request.CodYoAseguro))
                    //       && (string.IsNullOrEmpty(request.TituloReunion) || t.TituloReunion.Contains(request.TituloReunion)))
                    //   .ProjectTo<SearchYoAseguroDto>(_mapper.ConfigurationProvider)
                    //   .ToList();

                    //    VM.size = VM.data.Count;
                    //}
                    return VM;
            }
        }
    }
}
