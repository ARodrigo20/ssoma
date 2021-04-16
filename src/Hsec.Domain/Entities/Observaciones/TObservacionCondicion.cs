using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObservacionCondicion : AuditableEntity
    {
        public string CodObservacion { get; set; }
        public string CodTabla { get; set; }
        public string CondObservada { get; set; }
        public string CondAccionInmediata { get; set; }
        public string CodActiRelacionada { get; set; }
        public string CodHha { get; set; }
        public string CodCondicionSubEstandar { get; set; }
        public string CodStopWork { get; set; }
        public string CodCorreccion { get; set; }
        public string CodSubTipo { get; set; }

    }
}
