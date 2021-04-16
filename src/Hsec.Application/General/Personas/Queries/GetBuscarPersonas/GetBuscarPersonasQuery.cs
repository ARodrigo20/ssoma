using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Models;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Personas.Queries.GetBuscarPersonas
{
    public class GetBuscarPersonasQuery : PaginateViewModel, IRequest<PersonasVM> 
    {
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Nombres { get; set; }
        public string CodTipoDocumento { get; set; }
        public int? CodPosicion { get; set; }
        public string NroDocumento { get; set; }
        public string CodTipoPersona { get; set; }


        public class GetTodosQueryHandler : IRequestHandler<GetBuscarPersonasQuery, PersonasVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            

            public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PersonasVM> Handle(GetBuscarPersonasQuery request, CancellationToken cancellationToken)
            {
                var vm = new PersonasVM();
                IList<string> JerarquiaPersonas = new List<string>();
                if (request.CodPosicion != null) {
                    var JerPosicion = await _context.TJerarquia.FindAsync(request.CodPosicion);
                    
                   JerarquiaPersonas = _context.TJerarquia.Join(_context.TJerarquiaPersona, jer => jer.CodPosicion, jper => jper.CodPosicion, (jer, jper) => new { jer = jer, jper = jper })
                        .Where(tuple => (tuple.jer.PathJerarquia.Substring(0, JerPosicion.PathJerarquia.Length) == JerPosicion.PathJerarquia && tuple.jper.CodTipoPersona == 1))
                        .Select(t => t.jper.CodPersona)
                        .ToList();
                    

                }
                var filterListQuery = _context.TPersona.Where(per =>(String.IsNullOrEmpty(request.CodTipoPersona) || per.CodTipoPersona.Equals(request.CodTipoPersona))
                            && (String.IsNullOrEmpty(request.ApePaterno) || per.ApellidoPaterno.Contains(request.ApePaterno))
                            && (String.IsNullOrEmpty(request.ApeMaterno) || per.ApellidoMaterno.Contains(request.ApeMaterno))
                            && (String.IsNullOrEmpty(request.Nombres) || per.Nombres.Contains(request.Nombres))
                            && (request.CodPosicion == null || JerarquiaPersonas.Contains(per.CodPersona))
                            && (String.IsNullOrEmpty(request.CodTipoDocumento) || per.CodTipDocIden.Equals(request.CodTipoDocumento))
                            && (String.IsNullOrEmpty(request.NroDocumento) || per.NroDocumento.Contains(request.NroDocumento))
                        );

                vm.Count = filterListQuery.Count();

                if ((request.Pagina) > (vm.Count / request.PaginaTamanio) + 1) request.Pagina = 1;
                
                var ListQuery = filterListQuery
                        .Skip(request.Pagina * request.PaginaTamanio - request.PaginaTamanio)
                        .Take(request.PaginaTamanio)
                        .ProjectTo<PersonasDto>(_mapper.ConfigurationProvider)
                        .ToList();

                //foreach (var tuple in ListQuery)
                //{
                //    var destination = _mapper.Map<TPersona, PersonasDto>(tuple.per);
                //    destination.TipoDocumentos = _mapper.Map <TTipoDocPersona, TipoDocDto > (tuple.tipDoc);
                //    persDto.Add(destination);
                //}

                vm.Lists = ListQuery;
                
                

                return vm;
            }
        }
    }
}
