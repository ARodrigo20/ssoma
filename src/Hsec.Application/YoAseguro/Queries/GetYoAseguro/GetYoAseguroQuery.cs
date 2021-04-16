using AutoMapper;
using Hsec.Application.YoAseguro.Models;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Entities.YoAseguro;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.YoAseguro.Queries.GetYoAseguro
{
    public class GetYoAseguroQuery : IRequest<YoAseguroDto>
    {
        public string CodYoAseguro { get; set; }
        public class GetYoAseguroQueryHandler : IRequestHandler<GetYoAseguroQuery, YoAseguroDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetYoAseguroQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<YoAseguroDto> Handle(GetYoAseguroQuery request, CancellationToken cancellationToken)
            {
                TYoAseguro obj = _context.TYoAseguro.Find(request.CodYoAseguro);

                if (obj != null && obj.Estado == false) throw new NotFoundException("YoAseguro", request.CodYoAseguro);
                obj.PersonasReconocidas = _context.TPersonaYoAseguro
                    .Where(t => t.Estado == true && t.CodYoAseguro.Equals(request.CodYoAseguro))
                    .ToHashSet();

                YoAseguroDto yoAseguroResponse = _mapper.Map<TYoAseguro, YoAseguroDto>(obj);
                PersonaVM _per;
                foreach(var persona in yoAseguroResponse.PersonasReconocidas)
                {
                    _per = new PersonaVM();
                    //_per = await _persons.GetNombreYApellidos(persona.CodPersona);
                    _per = await _mediator.Send(new GetPersonaQuery() { CodPersona = persona.CodPersona });
                    if ( _per != null)
                    {
                        persona.Nombres = _per.Nombres;
                        persona.ApellidoPaterno = _per.ApellidoPaterno;
                        persona.ApellidoMaterno = _per.ApellidoMaterno;
                    }
                }
                return yoAseguroResponse;
                //return _mapper.Map<TYoAseguro, YoAseguroDto>(obj);
            }
        }
    }
}
