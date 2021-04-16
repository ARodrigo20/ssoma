using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.Inspecciones;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.Inspeccion.Models;
using AutoMapper.QueryableExtensions;
using Hsec.Application.General.Ubicaciones.Queries.Code2Name;
using Hsec.Application.General.Personas.Queries.GetCode2NameOne;

namespace Hsec.Application.Inspeccion.Queries.GetInspeccion
{
    public class GetInspeccionQuery : IRequest<GetInspeccionVM>
    {
        public string CodInspeccion { get; set; }
        public class GetInspeccionQueryHandler : IRequestHandler<GetInspeccionQuery, GetInspeccionVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetInspeccionQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<GetInspeccionVM> Handle(GetInspeccionQuery request, CancellationToken cancellationToken)
            {
                GetInspeccionVM VM = new GetInspeccionVM();

                TInspeccion inspeccion = _context.TInspeccion.Find(request.CodInspeccion);

                if (inspeccion != null && inspeccion.Estado == false) throw new NotFoundException("Inspeccion", request.CodInspeccion);
                
                VM.Inspeccion = _mapper.Map<TInspeccion, InspeccionDto>(inspeccion);
                VM.DetalleInspeccion = _context.TDetalleInspeccion
                    .Where(t => t.Estado == true && t.CodInspeccion.Equals(request.CodInspeccion))
                    .ProjectTo<DetalleInspeccionDto>(_mapper.ConfigurationProvider)
                    .ToList();
                foreach (var detalle in VM.DetalleInspeccion)
                {
                    if (!string.IsNullOrEmpty(detalle.CodUbicacion))
                    {
                        //detalle.DesUbicacion = await _general.GetUbicaciones(detalle.CodUbicacion);
                        detalle.DesUbicacion = await _mediator.Send(new Code2NameUbicacionQuery() { codigo = detalle.CodUbicacion }); 
                    }
                }

                VM.EquipoInspeccion = _context.TEquipoInspeccion
                    .Where(t => t.Estado == true && t.CodInspeccion.Equals(request.CodInspeccion))
                    .ProjectTo<EquipoDto>(_mapper.ConfigurationProvider)
                    .ToList();
                foreach (var persona in VM.EquipoInspeccion)
                {
                    //var nombreYapellidos = await _general.GetPersonas(persona.CodPersona);
                    var nombreYapellidos = await _mediator.Send(new GetCode2NameOneQuery() { code = persona.CodPersona });
                    if (nombreYapellidos != null)
                        persona.Nombres = nombreYapellidos;
                }
                VM.PersonasAtendidas = _context.TPersonaAtendida
                    .Where(t => t.Estado == true && t.CodInspeccion.Equals(request.CodInspeccion))
                    .ProjectTo<AtendidosDto>(_mapper.ConfigurationProvider)
                    .ToList();
                foreach (var personasAtendida in VM.PersonasAtendidas)
                {
                    //var nombreYapellidos = await _general.GetPersonas(personasAtendida.CodPersona);
                    var nombreYapellidos = await _mediator.Send(new GetCode2NameOneQuery() { code = personasAtendida.CodPersona });
                    if (nombreYapellidos != null)
                        personasAtendida.Nombres = nombreYapellidos;
                }
                VM.AnalisisCausa = _context.TInspeccionAnalisisCausa
                    .Where(t => t.Estado == true && t.CodInspeccion.Equals(request.CodInspeccion))
                    .ProjectTo<InspeccionAnalisisCausaDto>(_mapper.ConfigurationProvider)
                    .ToList();

                return VM;

            }
        }
    }
}
