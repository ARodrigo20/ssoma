using Hsec.Domain.Common;
using System;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.Simulacros
{
    public class TSimulacro : AuditableEntity
    {
        public string CodSimulacro { get; set; }
        public string CodTabla { get; set; }
        public string CodUbicacion { get; set; }
        public string CodPosicionGer { get; set; }
        public string CodRespGerencia { get; set; }
        public string CodPosicionSup { get; set; }
        public string CodRespSuperint { get; set; }
        public string CodContrata { get; set; }
        public string Suceso { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Hora { get; set; }
        public string HoraInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string HoraFinalizacion { get; set; }
        public string TiempoRespuesta { get; set; }
        public string Conclusiones { get; set; }

        public List<TEquipoSimulacro> EquipoSimulacro { get; set; }
        public List<TObservacionSimulacro> Observaciones { get; set; }
        public List<TRegEncuestaSimulacro> RegistroEncuesta { get; set; }
    }
}