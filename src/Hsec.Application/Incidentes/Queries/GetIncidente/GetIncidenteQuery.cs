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

namespace Hsec.Application.Incidentes.Queries.GetIncidente
{
    
    public class GetIncidenteQuery : IRequest<IncidenteDto> 
    {
        public string CodIncidente { get; set; }
        public class GetIncidenteQueryHandler : IRequestHandler<GetIncidenteQuery, IncidenteDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIncidenteQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IncidenteDto> Handle(GetIncidenteQuery request, CancellationToken cancellationToken)
            {
                try{

                    var VM = new IncidenteDto();
                    var CODIGO_INCIDENTE = request.CodIncidente;

                    TIncidente objTIncidente = _context.TIncidente.Find(request.CodIncidente);

                    if (objTIncidente == null) throw new NotFoundException("Incidente",request.CodIncidente);

                    VM.CodIncidente = request.CodIncidente;

                    
                    //datos generales
                    VM.DatosGenerales = _mapper.Map<TIncidente,DatosGeneralesDto>(objTIncidente);
                
                    //analisis causa
                    VM.AnalisisCausalidad = _context.TIncidenteAnalisisCausa
                        .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                        .ProjectTo<AnalisisCausalidadDto>(_mapper.ConfigurationProvider)
                        .ToList();

                    //detalle
                    VM.Detalle = _mapper.Map<DetalleDto>(objTIncidente);
                
                    VM.Detalle.EquipoInvestigacion = _context.TEquipoInvestigacion
                        .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                        .ProjectTo<EquipoInvestigacionDetalleDto>(_mapper.ConfigurationProvider)
                        .ToList();

                    VM.Detalle.SecuenciaEventos = _context.TSecuenciaEvento
                        .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                        .ProjectTo<SecuenciaEventosDetalleDto>(_mapper.ConfigurationProvider)
                        .ToList();

                    VM.Detalle.TestigosInvolucrados = _context.TTestigoInvolucrado
                        .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                        .ProjectTo<TestigosInvolucradosDetalleDto>(_mapper.ConfigurationProvider)
                        .ToList();

                    //ICAM
                    VM.ICAM = _context.TIcam
                        .Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE))
                        .ProjectTo<ICAMDto>(_mapper.ConfigurationProvider)
                        .ToList();

                    //Estado Incidente
                    VM.EstadoIncidente = objTIncidente.EstadoIncidente;

                    //_mapper.Map<IList<ICAMDto>, IList<TIcam>>(dataInc.ICAM);

                    //Afectados
                    var DetalleAfectado = _context.TDetalleAfectado.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE)).FirstOrDefault();
                    VM.AfectadosDescipcionesAcciones = _mapper.Map<TDetalleAfectado,AfectadosDescipcionesAccionesDto>(DetalleAfectado);

                    var listAC = _context.TAfectadoComunidad.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                    foreach (var item in listAC)
                    {
                        var obj = new DetalleAfectadoDto();
                        obj.Comunidad = _mapper.Map<ComunidadAfectadoDto>(item);
                        obj.tipoAfectado = TipoAfectado.Comunidad;
                        VM.AfectadosDescipcionesAcciones.Afectados.Add(obj);
                    }

                    var listMA = _context.TAfectadoMedioAmbiente.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                    foreach (var item in listMA)
                    {
                        var obj = new DetalleAfectadoDto();
                        obj.MedioAmbiente = _mapper.Map<MedioAmbienteAfectadoDto>(item);
                        obj.tipoAfectado = TipoAfectado.Medio_Ambiente;
                        VM.AfectadosDescipcionesAcciones.Afectados.Add(obj);
                    }

                    var listProp = _context.TAfectadoPropiedad.Where(t => t.CodIncidente.Equals(CODIGO_INCIDENTE));
                    foreach (var item in listProp)
                    {
                        var obj = new DetalleAfectadoDto();
                        obj.Propiedad = _mapper.Map<PropiedadAfectadoDto>(item);
                        obj.tipoAfectado = TipoAfectado.Propiedad;
                        VM.AfectadosDescipcionesAcciones.Afectados.Add(obj);
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
                        VM.AfectadosDescipcionesAcciones.Afectados.Add(obj);
                    }


                    return VM;
                }
                catch(System.Exception e){
                    var ee = e;
                    throw;
                }
            }
        }

    }
}