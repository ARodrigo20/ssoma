using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TAfectadoMedioAmbiente: AuditableEntity
    {
        public string CodIncidente { get; set; }
        public int Correlativo { get; set; }
        public string CodImpAmbiental { get; set; }
        public string Descripcion { get; set; }
        public string CodTipoAfectado { get; set; }


        //public virtual TImpactoAmbiental CodImpAmbientalNavigation { get; set; }
        public virtual TIncidente CodIncidenteNavigation { get; set; }
    }
}
