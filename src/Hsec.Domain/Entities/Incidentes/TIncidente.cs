using Hsec.Domain.Common;
using System;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TIncidente : AuditableEntity
    {
        public TIncidente () {
            TafectadoComunidad = new HashSet<TAfectadoComunidad> ();
            TafectadoMedioAmbiente = new HashSet<TAfectadoMedioAmbiente> ();
            TafectadoPropiedad = new HashSet<TAfectadoPropiedad> ();
            TdetalleAfectado = new HashSet<TDetalleAfectado> ();
            TdiasPerdidosAfectado = new HashSet<TDiasPerdidosAfectado> ();
            TequipoInvestigacion = new HashSet<TEquipoInvestigacion> ();
            Ticam = new HashSet<TIcam> ();
            TincidenteAnalisisCausa = new HashSet<TIncidenteAnalisisCausa> ();
            TinvestigaAfectado = new HashSet<TInvestigaAfectado> ();
            TsecuenciaEvento = new HashSet<TSecuenciaEvento> ();
            TtestigoInvolucrado = new HashSet<TTestigoInvolucrado> ();
        }

        // public long Correlativo { get; set; }
        public string CodIncidente { get; set; }
        public string CodTabla { get; set; }
        public string CodTituloInci { get; set; }
        public string DescripcionIncidente { get; set; }
        public string CodPosicionGer { get; set; }
        public string CodRespGerencia { get; set; }
        public string CodPosicionSup { get; set; }
        public string CodRespSuperint { get; set; }
        public string CodProveedor { get; set; }
        public string CodRespProveedor { get; set; }
        public string CodPerReporta { get; set; }
        public string CodAreaHsec { get; set; }
        public DateTime? FechaDelSuceso { get; set; }
        public string HoraDelSuceso { get; set; }
        public string CodTurno { get; set; }
        public string CodUbicacion { get; set; }
        public string CodSubUbicacion { get; set; }
        public string CodUbicacionEspecifica { get; set; }
        public string DesUbicacion { get; set; }
        public string CodTipoIncidente { get; set; }
        public string CodSubTipoIncidente { get; set; }
        public int? CodClasificaInci { get; set; }
        public string CodClasiPotencial { get; set; }
        public string CodLesAper { get; set; }
        public int? CodMedioAmbiente { get; set; }
        public int? CodActiRelacionada { get; set; }
        public string CodHha { get; set; }
        public string ResumenInfMedico { get; set; }
        public string Conclusiones { get; set; }
        public string Aprendizajes { get; set; }
        public string CodGrupoRiesgo { get; set; }
        public string CodRiesgo { get; set; }
        public int? Oculto { get; set; }
        public string EstadoIncidente { get; set; }

        public virtual ICollection<TAfectadoComunidad> TafectadoComunidad { get; set; }
        public virtual ICollection<TAfectadoMedioAmbiente> TafectadoMedioAmbiente { get; set; }
        public virtual ICollection<TAfectadoPropiedad> TafectadoPropiedad { get; set; }
        public virtual ICollection<TDiasPerdidosAfectado> TdiasPerdidosAfectado { get; set; }
        public virtual ICollection<TInvestigaAfectado> TinvestigaAfectado { get; set; }
        public virtual ICollection<TDetalleAfectado> TdetalleAfectado { get; set; }

        public virtual ICollection<TIcam> Ticam { get; set; }

        public virtual ICollection<TIncidenteAnalisisCausa> TincidenteAnalisisCausa { get; set; }

        public virtual ICollection<TEquipoInvestigacion> TequipoInvestigacion { get; set; }
        public virtual ICollection<TSecuenciaEvento> TsecuenciaEvento { get; set; }
        public virtual ICollection<TTestigoInvolucrado> TtestigoInvolucrado { get; set; }
    }
}