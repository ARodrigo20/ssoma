using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Incidentes.Models;
using Hsec.Domain.Common;
using Hsec.Domain.Entities.Incidentes;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.Files.Commands.CreateFiles;

namespace Hsec.Application.Incidentes.Commands.CreateIncidente
{

    public class CreateIncidenteCommand : IRequest<string>
    {
        public IncidenteDto data { get; set; }
        public List<PlanVM> planAccion { get; set; }
        public IFormFileCollection Files { get; set; }

        public class CreateIncidenteCommandHandler : IRequestHandler<CreateIncidenteCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public CreateIncidenteCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<string> Handle(CreateIncidenteCommand request, CancellationToken cancellationToken)
            {
                var nuevo = new TIncidente();
                var dataInc = request.data;
                nuevo.CodIncidente = nextCod();

                //mapear datos generales
                nuevo = _mapper.Map(dataInc.DatosGenerales, nuevo);

                //mapear ICAN
                nuevo.Ticam = _mapper.Map<IList<ICAMDto>, IList<TIcam>>(dataInc.ICAM);
                int indexIcan = 0;
                foreach (var item in nuevo.Ticam) item.Correlativo = ++indexIcan;

                //mapear analisis causa
                if(dataInc.AnalisisCausalidad != null)
                    nuevo.TincidenteAnalisisCausa = _mapper.Map<IList<AnalisisCausalidadDto>, IList<TIncidenteAnalisisCausa>>(dataInc.AnalisisCausalidad);

                //mapear detalles
                if(dataInc.Detalle != null){
                    nuevo = _mapper.Map(dataInc.Detalle, nuevo);

                    nuevo.TequipoInvestigacion = _mapper.Map<IList<EquipoInvestigacionDetalleDto>, IList<TEquipoInvestigacion>>(dataInc.Detalle.EquipoInvestigacion);
                    int indexEI = 0;
                    foreach (var item in nuevo.TequipoInvestigacion) item.Correlativo = ++indexEI;
                    nuevo.TsecuenciaEvento = _mapper.Map<IList<SecuenciaEventosDetalleDto>, IList<TSecuenciaEvento>>(dataInc.Detalle.SecuenciaEventos);
                    int indexSE = 0;
                    foreach (var item in nuevo.TsecuenciaEvento) item.Correlativo = ++indexSE;
                    nuevo.TtestigoInvolucrado = _mapper.Map<IList<TestigosInvolucradosDetalleDto>, IList<TTestigoInvolucrado>>(dataInc.Detalle.TestigosInvolucrados);
                    int indexTI = 0;
                    foreach (var item in nuevo.TtestigoInvolucrado) item.Correlativo = ++indexTI;
                }

                //mapear afectados
                var nuevoDetalleAfectado = _mapper.Map<AfectadosDescipcionesAccionesDto, TDetalleAfectado>(dataInc.AfectadosDescipcionesAcciones);
                nuevo.TdetalleAfectado.Add(nuevoDetalleAfectado);

                foreach (var item in dataInc.AfectadosDescipcionesAcciones.Afectados)
                {

                    if (item.tipoAfectado == TipoAfectado.Comunidad)
                    {
                        var data = _mapper.Map<ComunidadAfectadoDto, TAfectadoComunidad>(item.Comunidad);
                        nuevo.TafectadoComunidad.Add(data);
                    }
                    else if (item.tipoAfectado == TipoAfectado.Medio_Ambiente)
                    {
                        var data = _mapper.Map<MedioAmbienteAfectadoDto, TAfectadoMedioAmbiente>(item.MedioAmbiente);
                        nuevo.TafectadoMedioAmbiente.Add(data);
                    }
                    else if (item.tipoAfectado == TipoAfectado.Propiedad)
                    {
                        var data = _mapper.Map<PropiedadAfectadoDto, TAfectadoPropiedad>(item.Propiedad);
                        nuevo.TafectadoPropiedad.Add(data);
                    }
                    else if (item.tipoAfectado == TipoAfectado.Persona)
                    {
                        var data = _mapper.Map<PersonaAfectadoDto, TInvestigaAfectado>(item.Persona);
                        nuevo.TinvestigaAfectado.Add(data);
                        var diasPerdidiosAfectados = _mapper.Map<ICollection<MenAfectadoPersonaDto>, ICollection<TDiasPerdidosAfectado>>(item.Persona.Men);
                        int i = 1;
                        foreach (var itemDPA in diasPerdidiosAfectados)
                        {
                            itemDPA.CodPersona = data.DocAfectado;
                            itemDPA.Correlativo = i++;
                        }
                        nuevo.TdiasPerdidosAfectado = diasPerdidiosAfectados;
                    }
                    else
                    {
                        throw new NotFoundException("", "");
                    }
                }

                //estado cerrado o pentiente
                if(dataInc.EstadoIncidente != null && (dataInc.EstadoIncidente.Equals("P") || dataInc.EstadoIncidente.Equals("C")))
                {
                    nuevo.EstadoIncidente = dataInc.EstadoIncidente;
                }
                else
                {
                    nuevo.EstadoIncidente = "P";
                }

                int index = 1;
                foreach (var item in nuevo.TafectadoComunidad) item.Correlativo = index++;
                foreach (var item in nuevo.TafectadoMedioAmbiente) item.Correlativo = index++;
                foreach (var item in nuevo.TafectadoPropiedad) item.Correlativo = index++;
                foreach (var item in nuevo.TinvestigaAfectado) item.Correlativo = index++;

                _context.TIncidente.Add(nuevo);

                var r1 = await _context.SaveChangesAsync(cancellationToken);

                //var r2 = await _imagenes.Upload(request.Files, nuevo.CodIncidente);
                var r2 = await _mediator.Send(new CreateListFilesCommand { File = request.Files, NroDocReferencia = nuevo.CodIncidente, NroSubDocReferencia = nuevo.CodIncidente, CodTablaRef = "TINC" });

                //var r3 = await _planAccion.Create(request.planAccion, nuevo.CodIncidente);
                request.planAccion.ForEach(t => { t.docReferencia = nuevo.CodIncidente; t.docSubReferencia = nuevo.CodIncidente; });
                var r3 = await _mediator.Send(new CreatePlanAccionCommand() { planes = request.planAccion });

                return nuevo.CodIncidente;
            }


            public string nextCod()
            {
                var COD_INCIDENTE_MAX = _context.TIncidente.Max(t => t.CodIncidente);
                if (COD_INCIDENTE_MAX == null) COD_INCIDENTE_MAX = "INC00000001";
                else
                {
                    string numberStr = COD_INCIDENTE_MAX.Substring(3);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_INCIDENTE_MAX = String.Format("INC{0,8:00000000}", max);
                }
                return COD_INCIDENTE_MAX;
            }
            
        }

    }

}