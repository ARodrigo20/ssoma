using System.Collections.Generic;

namespace Hsec.Application.ReportePeligroFatal.Queries.GetReportesQuery
{
    public class ReportesVM
    {
        public IList<ReporteDto> List { get; set; }
        public int Count { get; set; }
    }
}