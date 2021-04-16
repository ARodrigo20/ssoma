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

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoBuscar
{
    
    public class GetIncidenteAfectadoBuscarQuery : IRequest<AfectadosVM> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteAfectadoQueryHandler : IRequestHandler<GetIncidenteAfectadoBuscarQuery, AfectadosVM>
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

            public async Task<AfectadosVM> Handle(GetIncidenteAfectadoBuscarQuery request, CancellationToken cancellationToken)
            {
                var VM = new AfectadosVM();
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