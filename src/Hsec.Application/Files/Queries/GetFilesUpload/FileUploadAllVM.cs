using System.Collections.Generic;

namespace Hsec.Application.Files.Queries.GetFilesUpload
{
    public class FileUploadAllVM
    {
        public FileUploadAllVM()
        {
            data = new List<FilesUploadOneVM>();
        }
        public IList<FilesUploadOneVM> data { get; set; }
        public int count { get; set; }

    }
}
