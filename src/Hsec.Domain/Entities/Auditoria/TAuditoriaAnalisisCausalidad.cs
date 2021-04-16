using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Auditoria
{
    public class TAuditoriaAnalisisCausalidad : AuditableEntity
    {
        public string CodCausa { get; set; }
        public string CodAnalisis { get; set; }
        public string CodHallazgo { get; set; }
        public string Comentario { get; set; }
        public string CodCondicion { get; set; }
    }
}