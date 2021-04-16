using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Cartilla.Queries.SearchCartilla
{
    public class SearchCartillaCovidQuery : IRequest<SearchCartillaVM>
    {
        public string CodCartilla { get; set; }
        // public string Des { get; set; }
        public string TipoCartilla { get; set; }
        public class SearchCartillaCovidQueryHandler : IRequestHandler<SearchCartillaCovidQuery, SearchCartillaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public SearchCartillaCovidQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SearchCartillaVM> Handle(SearchCartillaCovidQuery request, CancellationToken cancellationToken)
            {
                try{

                    SearchCartillaVM VM = new SearchCartillaVM();
                    ICollection<TCartilla> listTCartilla = _context.TCartilla
                    .Where(    t => t.Estado == true
                            && (string.IsNullOrEmpty(request.CodCartilla) || t.CodCartilla.Equals(request.CodCartilla))
                            && (t.PeligroFatal.Equals("CCPF0000022"))
                            && (string.IsNullOrEmpty(request.TipoCartilla) || t.TipoCartilla.Equals(request.TipoCartilla))
                            )
                    .ToList();

                    VM.list = _mapper.Map<ICollection<TCartilla>,ICollection<SearchCartillaDto> >(listTCartilla);
                    VM.size = VM.list.Count;
                    return VM;
                }
                catch(Exception e){
                    throw e;
                }
            }
        }

    }
}