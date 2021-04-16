using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.ReportePeligroFatal.Queries.GetDesviacionQuery
{
    public class DesviacionDto
    {
        public string Observacion { get; set; }
        public string CodCC { get; set; }
        public string DesCC { get; set; }
        public string CodCriterio { get; set; }
        public string Criterio { get; set; }
        public string Justificacion { get; set; }
        public string AccionCorrectiva { get; set; }
    }
}