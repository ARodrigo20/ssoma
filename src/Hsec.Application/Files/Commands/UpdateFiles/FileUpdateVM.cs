using System.Collections.Generic;

namespace Hsec.Application.Files.Commands.UpdateFiles
{

    public class FileListUpdateVM
    {
        public IList<FileUpdateVM> files { get; set; }     
    }
    public class FileUpdateVM
    {
        public int correlativoArchivos { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
