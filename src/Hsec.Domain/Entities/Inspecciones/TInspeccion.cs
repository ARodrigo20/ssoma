using System;
using System.Collections.Generic;
using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Inspecciones
{
    public class TInspeccion : AuditableEntity
    {
        public string CodInspeccion { get; set; }
        public string CodTabla { get; set; }
        public string CodTipo { get; set; }
        public string CodContrata { get; set; }
        public string AreaAlcance { get; set; }
        public DateTime? FechaP { get; set; }
        public DateTime? Fecha { get; set; }
        public string Hora { get; set; }
        public string Gerencia { get; set; }
        public string SuperInt { get; set; }
        public string CodUbicacion { get; set; }
        public string CodSubUbicacion { get; set; }
        public string Objetivo { get; set; }
        public string Conclusion { get; set; }
        public string Dispositivo { get; set; }

        public ICollection<TDetalleInspeccion> DetalleInspeccion { get; set; }
        public ICollection<TEquipoInspeccion> EquipoInspeccion { get; set; }
        public ICollection<TPersonaAtendida> PersonasAtendidas { get; set; }
        public ICollection<TInspeccionAnalisisCausa> AnalisisCausa { get; set; }
    }
}
