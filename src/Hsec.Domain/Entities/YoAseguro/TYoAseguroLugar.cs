using System;
using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.YoAseguro
{
    public class TYoAseguroLugar : AuditableEntity
    {
        public string CodUbicacion { get; set; }
        public string CodUbicacionPadre { get; set; }
        public string Descripcion { get; set; }
        public int DiasLaborados { get; set; }
        public int DiasDescanso { get; set; }
        public DateTime?  FechaReferencia { get; set; }
    }
}
