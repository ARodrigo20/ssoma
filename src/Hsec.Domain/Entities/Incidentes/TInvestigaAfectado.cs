using Hsec.Domain.Common;
using System;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TInvestigaAfectado: AuditableEntity
    {
        public long Correlativo { get; set; }
        public string CodIncidente { get; set; }
        public string CodTabla { get; set; }
        public string CodTipoAfectado { get; set; }
        public string DocAfectado { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Sexo { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public int? CodRegimen { get; set; }
        public string DiasDeTrabajo { get; set; }
        public string CodSisTrabajo { get; set; }
        public string PorcenDiasTrabajados { get; set; }
        public string HorasLaboradas { get; set; }
        public string CodGuardia { get; set; }
        public string CodExperiencia { get; set; }
        public string DesExperiencia { get; set; }
        public int? CodTipoPersona { get; set; }
        public int? CodZonasDeLesion { get; set; }
        public string ZonasDeLesion { get; set; }
        public string CodMecLesion { get; set; }
        public string CodNatLesion { get; set; }
        public int? CodClasificaInci { get; set; }
        public string NroAtencionMedia { get; set; }

        //public virtual TclasificacionIncidente CodClasificaInciNavigation { get; set; }
        //public virtual Tguardia CodGuardiaNavigation { get; set; }
        public virtual TIncidente CodIncidenteNavigation { get; set; }
        //public virtual TtipoAfectado CodTipoAfectadoNavigation { get; set; }
        //public virtual TzonasDeLesion CodZonasDeLesionNavigation { get; set; }
    }
}
