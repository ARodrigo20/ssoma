using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Auditoria
{
    public class TDatosHallazgo : AuditableEntity
    {
        public int Correlativo { get; set; }
        public string CodHallazgo { get; set; }
        public string CodTipoHallazgo { get; set; }
        public string Descripcion { get; set; }

    }
}