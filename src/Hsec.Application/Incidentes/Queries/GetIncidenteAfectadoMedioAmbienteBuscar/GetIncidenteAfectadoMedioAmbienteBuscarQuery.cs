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

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoBuscarMedioAmbiente
{
    
    public class GetIncidenteAfectadoBuscarMedioAmbienteQuery : IRequest<AfectadosMedioAmbienteVM> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAfectadoQueryHandler : IRequestHandler<GetIncidenteAfectadoBuscarMedioAmbienteQuery, AfectadosMedioAmbienteVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetIncidenteAfectadoQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<AfectadosMedioAmbienteVM> Handle(GetIncidenteAfectadoBuscarMedioAmbienteQuery request, CancellationToken cancellationToken)
            {
                var VM = new AfectadosMedioAmbienteVM();
                var CODIGO_INCIDENTE = request.CodIncidente;

                var listMA = _context.TAfectadoMedioAmbiente.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listMA)
                {
                    var obj = _mapper.Map<DetalleAfectadoDto>(item);
                    //obj.Impacto = await _general.GetMaestros("ImpactoAmbiental", obj.Impacto);
                    obj.Impacto = await _mediator.Send(new GetCode2NameQuery() { CodTable = "ImpactoAmbiental", CodMaestro = obj.Impacto });
                    VM.list.Add(obj);
                }

                return VM;
            }
        }

    }
}