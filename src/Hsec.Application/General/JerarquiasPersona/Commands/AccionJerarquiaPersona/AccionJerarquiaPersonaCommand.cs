using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.JerarquiasPersona.Commands.AccionJerarquiaPersona
{
    public class AccionJerarquiaPersonaCommand : IRequest<AccionJerarquiaPersonaVM>
    {
        public AccionJerarquiaPersonaVM modelVM { get; set; }
        public class AccionJerarquiaPersonaCommandHandler : IRequestHandler<AccionJerarquiaPersonaCommand, AccionJerarquiaPersonaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public AccionJerarquiaPersonaCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<AccionJerarquiaPersonaVM> Handle(AccionJerarquiaPersonaCommand request, CancellationToken cancellationToken)
            {
                AccionJerarquiaPersonaVM perReq = request.modelVM;
                int codPosicion = perReq.codPosicion;
                int pagina = perReq.pagina;
                int tamanio = perReq.tamanio;
                //AccionJerarquiaPersonaDto perReqDto = new AccionJerarquiaPersonaDto();

                List<AccionJerarquiaPersonaDto> jsonList = perReq.listaCodPer;
                List<string> codPersonas = new List<string>();

                foreach (var item in jsonList) { codPersonas.Add(item.codPer);  }                           

                var listaPer = _context.TJerarquiaPersona.Where(i=> codPersonas.Contains(i.CodPersona)).ToList();
                          
                var listJerarPer = (from jerPer in listaPer
                                        join jerarquias in _context.TJerarquia on jerPer.CodPosicion equals jerarquias.CodPosicion into lista
                                        from list in lista.DefaultIfEmpty()
                                        where list.CodPosicion == codPosicion
                                        select new AccionJerarquiaPersonaDto
                                        {
                                            codPer = jerPer.CodPersona                                            

                                        }).Skip(pagina * tamanio - tamanio)
                       .Take(tamanio).ToList();

                AccionJerarquiaPersonaVM nuevasPers = new AccionJerarquiaPersonaVM();
                nuevasPers.listaCodPer = listJerarPer;
                nuevasPers.codPosicion = codPosicion;
                nuevasPers.pagina = pagina;
                nuevasPers.tamanio = tamanio;               
               
                return nuevasPers;
            }
        }
    }
}
