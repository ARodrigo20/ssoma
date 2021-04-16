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

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPropiedadBuscar
{
    
    public class GetIncidenteAfectadoPropiedadBuscarQuery : IRequest<AfectadosPropiedadVM> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAfectadoPropiedadBuscarQueryHandler : IRequestHandler<GetIncidenteAfectadoPropiedadBuscarQuery, AfectadosPropiedadVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetIncidenteAfectadoPropiedadBuscarQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<AfectadosPropiedadVM> Handle(GetIncidenteAfectadoPropiedadBuscarQuery request, CancellationToken cancellationToken)
            {
                var VM = new AfectadosPropiedadVM();
                var CODIGO_INCIDENTE = request.CodIncidente;
                

                var listProp = _context.TAfectadoPropiedad.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listProp)
                {
                    var obj = _mapper.Map<DetalleAfectadoDto>(item);
                    //obj.Operador = await _general.GetPersonas(obj.Operador);
                    obj.Operador = await _mediator.Send(new GetCode2NameOneQuery() { code = obj.Operador });
                    //obj.Costo = await _general.GetMaestros("CostoDanio", obj.Costo);
                    obj.Costo = await _mediator.Send(new GetCode2NameQuery() { CodTable = "CostoDanio", CodMaestro = obj.Costo });
                    //obj.TipoActivo = await _general.GetMaestros("TipoActivo", obj.TipoActivo);
                    obj.TipoActivo = await _mediator.Send(new GetCode2NameQuery() { CodTable = "TipoActivo", CodMaestro = obj.TipoActivo });
                    VM.list.Add(obj);
                }

                return VM;
            }
        }

    }
}