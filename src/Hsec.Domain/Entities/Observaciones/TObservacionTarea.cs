using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Observaciones
{
    public class TObservacionTarea : AuditableEntity
    {

        public string CodObservacion { get; set; }
        public string TareaObservada { get; set; }
        public string Pet { get; set; }
        public string CodActiRelacionada { get; set; }
        public string CodHha { get; set; }
        public string CodErrorObs { get; set; }
        public string CodEstadoObs { get; set; }
        // public string DesConclusiones { get; set; }
        // public string DesConclusionesfeedback { get; set; }
        // public string DesConclusionesProModi { get; set; }
        public string ComenRecoOpor { get; set; }
        public string CodStopWork { get; set; }



        //public TObsMaestro HHARelacionada { get; set; }
        //public TObsMaestro ActividadRelacionada { get; set; }
        //public TObsMaestro StopWork { get; set; }
        //public TObsMaestro EstadoObs { get; set; }
        //public TObsMaestro ErrorObs { get; set; }

        //public virtual TactividadRelacionada CodActiRelacionadaNavigation { get; set; }
        //public virtual TerrorObservacion CodErrorObsNavigation { get; set; }
        //public virtual TestadoObservacion CodEstadoNavigation { get; set; }
        //public virtual Thharelacionada CodHhaNavigation { get; set; }
    }
}
