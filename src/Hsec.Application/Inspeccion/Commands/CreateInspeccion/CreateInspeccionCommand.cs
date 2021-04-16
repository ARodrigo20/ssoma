using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Application.Inspeccion.Models;
using Hsec.Domain.Entities.Inspecciones;
using Hsec.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.Files.Commands.CreateOneFile;

namespace Hsec.Application.Inspeccion.Commands.CreateInspeccion
{
    public class CreateInspeccionCommand : IRequest<Unit>
    {
        public InspeccionDto Inspeccion { get; set; }
        public List<EquipoDto> Equipo { get; set; }
        public List<AtendidosDto> Atendidos { get; set; }
        public List<DetalleInspeccionDto> Observaciones { get; set; }
        //public List<PlanAccionVM> Planes { get; set; }
        public List<InspeccionAnalisisCausaDto> AnalisisCausa { get; set; }
        public IFormFileCollection Files { get; set; }

        public class CreateInspeccionCommandHandler : IRequestHandler<CreateInspeccionCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public CreateInspeccionCommandHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<Unit> Handle(CreateInspeccionCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    if (request.Inspeccion == null) throw new Exception("falta Inspeccion");
                    TInspeccion entity = _mapper.Map<InspeccionDto, TInspeccion>(request.Inspeccion);
                    string max_cod = nextCod();
                    entity.CodInspeccion = max_cod;
                    _context.TInspeccion.Add(entity);

                    if (request.Observaciones != null)
                    {
                        foreach (var _detalle in request.Observaciones)
                        {
                            TDetalleInspeccion newDetalle = new TDetalleInspeccion();
                            newDetalle.CodInspeccion = max_cod;
                            newDetalle.CodTabla = _detalle.CodTabla;
                            newDetalle.Lugar = _detalle.Lugar;
                            newDetalle.CodUbicacion = _detalle.CodUbicacion;
                            newDetalle.CodAspectoObs = _detalle.CodAspectoObs;
                            newDetalle.CodActividadRel = _detalle.CodActividadRel;
                            newDetalle.Observacion = _detalle.Observacion;
                            newDetalle.CodNivelRiesgo = _detalle.CodNivelRiesgo;
                            _context.TDetalleInspeccion.Add(newDetalle);
                            var num1 = await _context.SaveChangesAsync(cancellationToken);

                            if (_detalle.PlanesAccion != null && _detalle.PlanesAccion.Count > 0)
                            {
                                // var num2 = await _planAccion.Create(_detalle.PlanesAccion, max_cod, newDetalle.Correlativo.ToString());
                                _detalle.PlanesAccion.ForEach(t => { t.docReferencia = max_cod; t.docSubReferencia = newDetalle.Correlativo.ToString(); });
                                var num2 = await _mediator.Send(new CreatePlanAccionCommand() { planes = _detalle.PlanesAccion });
                            }
                            if (request.Files != null)
                            {
                                foreach (var _file in request.Files)
                                {
                                    var decodeFileName = Base64Decode(_file.Name);
                                    var obj = JsonConvert.DeserializeObject<CreateFileAtributesVCCDto>(decodeFileName);

                                    if (obj.CodDetalle == _detalle.Correlativo.ToString())
                                    {
                                        //var num3 = await _imagenes.UploadOneFile(_file, max_cod, newDetalle.Correlativo.ToString());
                                        var num3 = await _mediator.Send(new CreateOneFileCommand { File = _file, NroDocReferencia = max_cod, NroSubDocReferencia = newDetalle.Correlativo.ToString(), CodTablaRef = "TINSP" });
                                    }

                                }
                            }
                        }

                    }

                    if (request.Equipo != null)
                    {
                        var nuevo_equipo_List = _mapper.Map<IList<EquipoDto>, IList<TEquipoInspeccion>>(request.Equipo);
                        foreach (var persona in nuevo_equipo_List)
                        {
                            persona.CodInspeccion = max_cod;
                            //_context.TDetalleInspeccion.Add(detalle);
                        }
                        _context.TEquipoInspeccion.AddRange(nuevo_equipo_List);
                    }

                    if (request.Atendidos != null)
                    {
                        var nuevo_atendidos_List = _mapper.Map<IList<AtendidosDto>, IList<TPersonaAtendida>>(request.Atendidos);
                        foreach (var persona in nuevo_atendidos_List)
                        {
                            persona.CodInspeccion = max_cod;
                            //_context.TDetalleInspeccion.Add(detalle);
                        }
                        _context.TPersonaAtendida.AddRange(nuevo_atendidos_List);
                    }

                    if (request.AnalisisCausa != null)
                    {
                        var nuevo_analisis_List = _mapper.Map<IList<InspeccionAnalisisCausaDto>, IList<TInspeccionAnalisisCausa>>(request.AnalisisCausa);
                        foreach (var analisis in nuevo_analisis_List)
                        {
                            analisis.CodInspeccion = max_cod;
                            //_context.TDetalleInspeccion.Add(detalle);
                        }
                        _context.TInspeccionAnalisisCausa.AddRange(nuevo_analisis_List);
                    }

                    var num4 = await _context.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public string nextCod()
            {
                var COD_INSPECCION_MAX = _context.TInspeccion.Max(t => t.CodInspeccion);

                if (COD_INSPECCION_MAX == null) COD_INSPECCION_MAX = "INSP0000000001";
                else
                {
                    string numberStr = COD_INSPECCION_MAX.Substring(4);
                    int max = Int32.Parse(numberStr) + 1;
                    COD_INSPECCION_MAX = String.Format("INSP{0,10:0000000000}", max);
                }
                return COD_INSPECCION_MAX;
            }

            public static string Base64Decode(string base64EncodedData)
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

        }
    }
}
