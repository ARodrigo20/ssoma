using Hsec.Domain.Common;
using System;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObservacion: AuditableEntity
    {
        public string CodObservacion { get; set; }
        public string CodPosicionGer { get; set; }
        public string CodPosicionSup { get; set; }
        public string CodAreaHsec { get; set; }
        //public TipoObservacion CodTipoObservacion { get; set; }
        public int CodTipoObservacion { get; set; }
        public string CodSubTipoObs { get; set; }
        public string CodNivelRiesgo { get; set; }
        public string CodObservadoPor { get; set; }
        public DateTime? FechaObservacion { get; set; }
        public string HoraObservacion { get; set; }
        public string CodUbicacion { get; set; } //?? 
        public string CodSubUbicacion { get; set; } //??
        public string CodUbicacionEspecifica { get; set; } //??
        public string DesUbicacion { get; set; }
    }
}
