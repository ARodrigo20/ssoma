using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetCode2NameOne;
using Hsec.Application.General.Maestro.Queries.GetCode2Name;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPersonaBuscar
{
    
    public class GetIncidenteAfectadoPersonaBuscaQuery : IRequest<AfectadosPersonaVM> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAfectadoPersonaQueryHandler : IRequestHandler<GetIncidenteAfectadoPersonaBuscaQuery, AfectadosPersonaVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetIncidenteAfectadoPersonaQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<AfectadosPersonaVM> Handle(GetIncidenteAfectadoPersonaBuscaQuery request, CancellationToken cancellationToken)
            {
                var VM = new AfectadosPersonaVM();
                var CODIGO_INCIDENTE = request.CodIncidente;
                
                var listPer = _context.TInvestigaAfectado.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listPer)
                {
                    var obj = _mapper.Map<DetalleAfectadoDto>(item);
                    //obj.Afectado = await _general.GetPersonas(obj.Afectado);
                    obj.Afectado = await _mediator.Send(new GetCode2NameOneQuery() { code = obj.Afectado });
                    //obj.ZonasLesion = await _general.GetMaestros("ZonasDeLesion", obj.ZonasLesion);
                    obj.ZonasLesion = await _mediator.Send(new GetCode2NameQuery() { CodTable = "ZonasDeLesion", CodMaestro = obj.ZonasLesion });
                    //obj.NatLesion = await _general.GetMaestros("NaturalezaLesion", obj.NatLesion);
                    obj.NatLesion = await _mediator.Send(new GetCode2NameQuery() { CodTable = "NaturalezaLesion", CodMaestro = obj.NatLesion });
                    //obj.Experiencia = await _general.GetMaestros("Experiencia", obj.Experiencia);
                    obj.Experiencia = await _mediator.Send(new GetCode2NameQuery() { CodTable = "Experiencia", CodMaestro = obj.Experiencia });
                    VM.list.Add(obj);
                }

                return VM;
            }
        }

    }
}