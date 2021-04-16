using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Hsec.Application.Common.Interfaces;
using System.Collections.Generic;
using Hsec.Application.General.Jerarquias.Queries.GetJerarquiasPersonaAll;
using Hsec.Application.Common.Models;
using System;

namespace Hsec.Application.General.JerarquiasPersona.Queries.GetJerarquiasPersonaAll
{
    public class GetJerarquiasPersonaAllQuery : IRequest<GeneralCollection<PersonaPosVM>>
    {       
        public int CodPosicion { get; set; }
        public class GetJerarquiasPersonaAllQueryHandler : IRequestHandler<GetJerarquiasPersonaAllQuery, GeneralCollection<PersonaPosVM>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            

            public GetJerarquiasPersonaAllQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GeneralCollection<PersonaPosVM>> Handle(GetJerarquiasPersonaAllQuery request, CancellationToken cancellationToken)
            {

                var JerPosicion = await _context.TJerarquia.FindAsync(request.CodPosicion);

                IEnumerable<JerarquiaPosVM> JerarquiaPersonas = _context.TJerarquia.Join(_context.TJerarquiaPersona, jer => jer.CodPosicion, jper => jper.CodPosicion, (jer, jper) => new { jer = jer, jper = jper })
                  .Where(tuple => (tuple.jer.PathJerarquia.Substring(0, JerPosicion.PathJerarquia.Length) == JerPosicion.PathJerarquia && tuple.jper.CodTipoPersona == 1))
                  .Select(t => new JerarquiaPosVM { 
                      CodPosicion= Convert.ToInt32(t.jer.CodPosicion),
                      CodPersona = t.jper.CodPersona,
                      Path= t.jer.PathJerarquia
                  })
                  .ToList();               

                var datos = JerarquiaPersonas.Join(_context.TPersona, jer => jer.CodPersona, jper => jper.CodPersona, (jer, jper) => new { jer = jer, jper = jper })
                  .Where(p => p.jper.Estado)
                  .Select(p => new PersonaPosVM(p.jer.CodPosicion,p.jer.Path, p.jper.CodPersona, p.jper.Nombres, p.jper.ApellidoPaterno, p.jper.ApellidoMaterno))
                  .ToList();
               // var data = _context.TPersona.Where(p => Personas.Contains(p.CodPersona)).Select(p => new PersonaPosVM(,p.CodPersona, p.Nombres, p.ApellidoPaterno, p.ApellidoMaterno)).ToList();               
                
                return new GeneralCollection<PersonaPosVM>(datos);
            }
        }

        public class JerarquiaPosVM
        {
            public string CodPersona { get; set; }
            public int CodPosicion { get; set; }
            public string Path { get; set; }
        }

    }
}
        




