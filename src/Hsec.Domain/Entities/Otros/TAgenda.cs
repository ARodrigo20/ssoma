using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Otros
{
    public class TAgenda : AuditableEntity
    {
        public string CodReunion { get; set; }

        public int Correlativo { get; set; }

        public string DesAgenda { get; set; }

        public TReunion Reunion { get; set; }
    }
}
