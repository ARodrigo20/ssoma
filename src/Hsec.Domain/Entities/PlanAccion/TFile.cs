using Hsec.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Hsec.Domain.Entities.PlanAccion
{
    public class TFile : AuditableEntity
    {
        [Key]
        public int CorrelativoArchivos { get; set; }
        public string NroDocReferencia { get; set; }
        public string NroSubDocReferencia { get; set; }
        public int? GrupoPertenece { get; set; }
        public byte[] ArchivoData { get; set; }
        public byte[] PreviewData { get; set; }
        public string Descripcion { get; set; }
        public string TipoArchivo { get; set; }
        public string Nombre { get; set; }
        public string CodTablaRef { get; set; }
    }
}
