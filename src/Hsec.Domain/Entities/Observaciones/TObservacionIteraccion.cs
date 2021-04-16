using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObservacionIteraccion : AuditableEntity
    {
        public string CodObservacion { get; set; }
        public string CodLiderPersona { get; set; }
        public string CodContratista { get; set; }
        public string EquipoInvolucrado { get; set; }
        public string DetalleComportamiento { get; set; }
        public string AccionesInmediatas { get; set; }
        public string IteraccionSeguridad { get; set; }
        public string OtroActividadRiesgo { get; set; }
        public string OtroComponente { get; set; }
        public string ActividadTareaObs { get; set; }
        public string CodStopWork { get; set; }


    }
}
