using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.ValidadorFiles.Commands.VMs
{
    public class FileVM
    {
        public byte[] archivoData { get; set; }
        public string tipoArchivo { get; set; }
        public string nombre { get; set; }
    }
}
