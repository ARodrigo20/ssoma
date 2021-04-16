using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.General.Personas.Queries.GetPersona
{

    public class GetPersonaQuery : IRequest<PersonaVM>
    {
        public string CodPersona { get; set; }
        public class GetPersonaQueryHandler : IRequestHandler<GetPersonaQuery, PersonaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPersonaQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PersonaVM> Handle(GetPersonaQuery request, CancellationToken cancellationToken)
            {

                var Codpersona = request.CodPersona;
                //if (Codpersona.Contains(" "))
                //{
                //    var values = Codpersona.Split(" ");
                //    Codpersona = values[0].Length > values[1].Length ? values[0] : values[1];
                //}
                var persona = _context.TPersona.Where(t => t.CodPersona.Equals(Codpersona)).FirstOrDefault();
                if(persona==null) throw new NotFoundException("TPersona","no existe codigo");
                PersonaVM perVM = _mapper.Map<PersonaVM>(persona);
                return perVM;
            }
        }

    }
}