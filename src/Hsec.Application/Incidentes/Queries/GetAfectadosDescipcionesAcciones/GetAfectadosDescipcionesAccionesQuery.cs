using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Incidentes.Models;
using Hsec.Domain.Entities.Incidentes;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Incidentes.Queries.GetAfectadosDescipcionesAcciones
{

    public class GetAfectadosDescipcionesAccionesQuery : IRequest<AfectadosDescipcionesAccionesDto>
    {
        public string CodIncidente { get; set; }
        public class GetAfectadosDescipcionesAccionesQueryHandler : IRequestHandler<GetAfectadosDescipcionesAccionesQuery, AfectadosDescipcionesAccionesDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAfectadosDescipcionesAccionesQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AfectadosDescipcionesAccionesDto> Handle(GetAfectadosDescipcionesAccionesQuery request, CancellationToken cancellationToken)
            {
                var VM = new AfectadosDescipcionesAccionesDto();
                var CODIGO_INCIDENTE = request.CodIncidente;

                TIncidente objTIncidente = _context.TIncidente.Find(request.CodIncidente);

                if (objTIncidente == null || objTIncidente.Estado == false) throw new NotFoundException("Incidente", request.CodIncidente);

                
                //_mapper.Map<IList<ICAMDto>, IList<TIcam>>(dataInc.ICAM);
                VM = _context.TDetalleAfectado
                    .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                    .ProjectTo<AfectadosDescipcionesAccionesDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();

                var listAC = _context.TAfectadoComunidad.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listAC)
                {
                    var obj = new DetalleAfectadoDto();
                    obj.Comunidad = _mapper.Map<ComunidadAfectadoDto>(item);
                    obj.tipoAfectado = TipoAfectado.Comunidad;
                    VM.Afectados.Add(obj);
                }

                var listMA = _context.TAfectadoMedioAmbiente.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listMA)
                {
                    var obj = new DetalleAfectadoDto();
                    obj.MedioAmbiente = _mapper.Map<MedioAmbienteAfectadoDto>(item);
                    obj.tipoAfectado = TipoAfectado.Medio_Ambiente;
                    VM.Afectados.Add(obj);
                }

                var listProp = _context.TAfectadoPropiedad.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listProp)
                {
                    var obj = new DetalleAfectadoDto();
                    obj.Propiedad = _mapper.Map<PropiedadAfectadoDto>(item);
                    obj.tipoAfectado = TipoAfectado.Propiedad;
                    VM.Afectados.Add(obj);
                }

                var listPersona = _context.TInvestigaAfectado.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                foreach (var item in listPersona)
                {
                    var obj = new DetalleAfectadoDto();
                    obj.Persona = _mapper.Map<PersonaAfectadoDto>(item);
                    obj.tipoAfectado = TipoAfectado.Persona;
                    obj.Persona.Men = _context.TDiasPerdidosAfectado
                        .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE) && t.CodPersona.Equals(obj.Persona.DocAfectado))
                        .ProjectTo<MenAfectadoPersonaDto>(_mapper.ConfigurationProvider)
                        .ToList();

                    VM.Afectados.Add(obj);
                }



                return VM;
            }
        }

    }
}