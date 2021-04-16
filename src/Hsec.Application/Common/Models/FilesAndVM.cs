using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Common.Models
{
    public class FilesAndVM
    {
        public IFormCollection files { get; set; }
        public string dataJSON { get; set; }
        //public string dataPlanAccionJSON { get; set; }
        public string dataFilesJSON { get; set; }
    }
}
