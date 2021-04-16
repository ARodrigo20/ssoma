using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TCartilla : AuditableEntity
    {
        public TCartilla()
        {
            Detalle = new HashSet<TCartillaDetalle>();
        }

        public string CodCartilla { get; set; }
        public string DesCartilla { get; set; }
        public string TipoCartilla { get; set; }
        public string PeligroFatal { get; set; }
        public string Modulo { get; set; }
        public ICollection<TCartillaDetalle> Detalle { get; set; }

    }
}
