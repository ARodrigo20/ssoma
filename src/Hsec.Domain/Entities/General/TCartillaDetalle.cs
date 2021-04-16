using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.General
{
    public class TCartillaDetalle : AuditableEntity
    {
      
        public string CodCartillaDet { get; set; }
        public string CodCartilla { get; set; }
        public string CodCC { get; set; }

        public TCartilla Cartilla { get; set; }
        public TControlCritico CC { get; set; }
    }
}
