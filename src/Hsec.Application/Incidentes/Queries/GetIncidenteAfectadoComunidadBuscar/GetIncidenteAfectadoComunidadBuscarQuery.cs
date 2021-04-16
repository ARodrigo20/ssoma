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
using Hsec.Application.General.Maestro.Queries.GetCode2Name;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoComunidadBuscar
{
    
    public class GetIncidenteAfectadoComunidadBuscarQuery : IRequest<AfectadosComunidadVM> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAfectadoComunidadQueryHandler : IRequestHandler<GetIncidenteAfectadoComunidadBuscarQuery, AfectadosComunidadVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetIncidenteAfectadoComunidadQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<AfectadosComunidadVM> Handle(GetIncidenteAfectadoComunidadBuscarQuery request, CancellationToken cancellationToken)
            {
                var VM = new AfectadosComunidadVM();
                var CODIGO_INCIDENTE = request.CodIncidente;
                
                var listCom = _context.TAfectadoComunidad.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listCom)
                {
                    var obj = _mapper.Map<DetalleAfectadoDto>(item);
                    //obj.Comunidad = await _general.GetMaestros("ComuAfectada", obj.Comunidad);
                    obj.Comunidad = await _mediator.Send(new GetCode2NameQuery() { CodTable = "ComuAfectada", CodMaestro = obj.Comunidad });
                    //obj.Motivo = await _general.GetMaestros("Motivo", obj.Motivo);
                    obj.Motivo = await _mediator.Send(new GetCode2NameQuery() { CodTable = "Motivo", CodMaestro = obj.Motivo });
                    VM.list.Add(obj);
                }

                return VM;
            }
        }

    }
}