using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TModulo : AuditableEntity
    {
        public TModulo()
        {
            this.Hijos = new HashSet<TModulo>();
        }

        public string CodModulo { get; set; }
        public string CodModuloPadre { get; set; }
        public string Descripcion { get; set; }
        public TModulo Padre { get; set; }
        public ICollection<TModulo> Hijos { get; set; }
    }
}