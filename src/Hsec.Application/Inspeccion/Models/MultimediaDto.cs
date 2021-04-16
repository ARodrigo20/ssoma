using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Inspeccion.Models
{
    public class MultimediaDto
    {
        public int Correlativo { get; set; }
        public string CodTabla { get; set; }
        public string NroDocReferencia { get; set; }
        public int? GrupoPertenece { get; set; }
        public byte[] ArchivoData { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public string TipoArchivo { get; set; }
        public string Estado { get; set; }
    }
}
