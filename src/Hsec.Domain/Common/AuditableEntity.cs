using System;

namespace Hsec.Domain.Common
{
    public class AuditableEntity
    {
        public string CreadoPor { get; set; }
        public DateTime Creado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? Modificado { get; set; }
        public bool Estado { get; set; }
    }
}
