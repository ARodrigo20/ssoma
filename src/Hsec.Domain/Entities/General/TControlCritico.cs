using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
    public class TControlCritico : AuditableEntity
    {
        public TControlCritico()
        {
            Criterios = new HashSet<TCriterio>();
        }

        public string CodCC { get; set; }
        public string CodRiesgo { get; set; }
        public string DesCC { get; set; }
        public string TipoCC { get; set; }
        public string PeligroFatal { get; set; }
        public string Modulo { get; set; }

        public ICollection<TCriterio> Criterios { get; set; }
        public ICollection<TCartillaDetalle> CartillaDetalles { get; set; }
    }
}