using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.ReportePeligroFatal.Queries.GetControlesQuery
{
    public class ControlDto
    {
        public int CodReportePF { get; set; }
        public string CodCC { get; set; }
        public string DesCC { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string Observacion { get; set; }
        public double Efectividad { get; set; }
    }
}