using Hsec.Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hsec.Application.Common.Interfaces
{
    public interface IReportService
    {
        public byte[] GetReport(string ReportName,List<Tuple<string,string>> Parametros, ExportFormat Format,string OtherSetting);
        public Task<byte[]> GetReport2(string ReportName, List<Tuple<string, string>> Parametros, ExportFormat Format, string OtherSetting);
        //public Task<byte[]> GetReportAsync(string ReportName, List<Tuple<string, string>> Parametros, ExportFormat Format, string OtherSetting);
    }
}
