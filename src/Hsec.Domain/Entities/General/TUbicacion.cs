using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TUbicacion : AuditableEntity
    {
        public TUbicacion()
        {
            this.Hijos = new HashSet<TUbicacion>();
        }

        public string CodUbicacion { get; set; }
        public string CodUbicacionPadre { get; set; }
        public string Descripcion { get; set; }
        public TUbicacion Padre { get; set; }
        public ICollection<TUbicacion> Hijos { get; set; }
    }
}