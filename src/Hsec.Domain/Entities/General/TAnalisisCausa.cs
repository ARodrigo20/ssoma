using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TAnalisisCausa : AuditableEntity
    {
        public TAnalisisCausa()
        {
            this.Hijos = new HashSet<TAnalisisCausa>();
        }

        public string CodAnalisis { get; set; }
        public string CodPadre { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public string CodAnterior { get; set; }
        public TAnalisisCausa Padre { get; set; }
        public ICollection<TAnalisisCausa> Hijos { get; set; }
    }
}