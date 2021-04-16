using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TGestionRiesgo : AuditableEntity
    {
        public TGestionRiesgo()
        {
            this.Hijos = new HashSet<TGestionRiesgo>();
        }

        public string CodGestionRiesgo { get; set; }
        public string CodGestionRiesgoPadre { get; set; }
        public string Descripcion { get; set; }
        public string DetalleAsociado { get; set; }
        public TGestionRiesgo Padre { get; set; }
        public ICollection<TGestionRiesgo> Hijos { get; set; }
    }
}