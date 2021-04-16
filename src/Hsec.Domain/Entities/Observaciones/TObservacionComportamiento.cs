using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObservacionComportamiento : AuditableEntity
    {
        public string CodObservacion { get; set; }
        public string CompObservada { get; set; }
        public string CompAccionInmediata { get; set; }
        public string CodActiRelacionada { get; set; }
        public string CodEstado { get; set; }
        public string CodHha { get; set; }
        public string CodErrorObs { get; set; }
        public string CodActoSubEstandar { get; set; }
        public string CodStopWork { get; set; }
        public string CodCorreccion { get; set; }
        //public string CodEPP { get; set; }
        //public string CodTipoProteccion { get; set; }
        public string CodSubTipo { get; set; }

    }
}
