using Hsec.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Incidentes.Models
{
    public class IncidenteDto 
    {
        public string CodIncidente { get; set; }
        public string CodTabla { get; set; }
        public DatosGeneralesDto DatosGenerales { get; set; }
        public AfectadosDescipcionesAccionesDto AfectadosDescipcionesAcciones { get; set; }
        public DetalleDto Detalle { get; set; }
        public IList<AnalisisCausalidadDto> AnalisisCausalidad { get; set; }
        public IList<ICAMDto> ICAM { get; set; }
        public string EstadoIncidente { get; set; }

        public IncidenteDto()
        {
            DatosGenerales = new DatosGeneralesDto();
            AfectadosDescipcionesAcciones = new AfectadosDescipcionesAccionesDto();
            Detalle = new DetalleDto();
            AnalisisCausalidad = new List<AnalisisCausalidadDto>();
            ICAM = new List<ICAMDto>();
        }

    }
}
