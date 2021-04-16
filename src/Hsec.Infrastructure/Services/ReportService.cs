using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Enums;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using Hsec.Application.Common.Exceptions;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Hsec.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly IConfiguration _config;
        private readonly string _baseURL;
        NetworkCredential UserNTL;
        public ReportService(IConfiguration configuration)
        {
            _config = configuration;
            _baseURL = _config["appSettings:ReportSettings:BaseUrl"];

            UserNTL = new NetworkCredential(
                   _config["appSettings:ReportSettings:NTL:userName"],
                   _config["appSettings:ReportSettings:NTL:password"],
                   _config["appSettings:ReportSettings:NTL:domain"]);

        }
        private static string GetExportFormatString(ExportFormat f)
        {
            switch (f)
            {
                case ExportFormat.XML: return "XML";
                case ExportFormat.CSV: return "CSV";
                case ExportFormat.Image: return "IMAGE";
                case ExportFormat.PDF: return "PDF";
                case ExportFormat.MHTML: return "MHTML";
                case ExportFormat.HTML4: return "HTML4.0";
                case ExportFormat.HTML32: return "HTML3.2";
                case ExportFormat.Excel: return "EXCEL";
                case ExportFormat.Word: return "WORD";

                default:
                    return "PDF";
            }
        }
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
        public byte[] GetReport(string ReportName, List<Tuple<string, string>> Parametros, ExportFormat Format, string OtherSetting)
        {

            string paramets = "";
            string ConfigRep = "&rs:Command=Render&rs:Format=" + GetExportFormatString(Format) + OtherSetting;
            foreach (Tuple<string, string> param in Parametros)
                paramets += "&" + param.Item1 + "=" + param.Item2;
            string URL = _baseURL + ReportName + ConfigRep + paramets;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                //request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
                request.PreAuthenticate = true;
                //request.UseDefaultCredentials = true;
                //request.Proxy.Credentials = UserNTL;
                request.Credentials = UserNTL;
                HttpWebResponse HttpWResp = (HttpWebResponse)request.GetResponse();
                Stream fStream = HttpWResp.GetResponseStream();
                return ReadFully(fStream);                            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<byte[]> GetReport2(string ReportName, List<Tuple<string, string>> Parametros, ExportFormat Format, string OtherSetting)
        {

            string paramets = "";
            string ConfigRep = "&rs:Command=Render&rs:Format=" + GetExportFormatString(Format) + OtherSetting;
            foreach (Tuple<string, string> param in Parametros)
                paramets += "&" + param.Item1 + "=" + param.Item2;
            string URL = _baseURL + ReportName + ConfigRep + paramets;


            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            clientHandler.Credentials = UserNTL;
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, URL);
                var informacionAMandar = JsonConvert.SerializeObject(Parametros,
                                    Formatting.None,
                                    new JsonSerializerSettings
                                    {
                                        NullValueHandling = NullValueHandling.Ignore
                                    });
                request.Content = new StringContent(informacionAMandar, Encoding.UTF8, "application/json");//CONTENT-TYPE header
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Stream fStream = await response.Content.ReadAsStreamAsync();
                    return ReadFully(fStream);
                }

                else { new ExceptionGeneral("respuesta del servidor Incorrecta !"); return null; }
            }

            //using (HttpClient client = new HttpClient())
            //{
            //    var response = await client.GetAsync(URL, HttpCompletionOption.ResponseHeadersRead);

            //    response.EnsureSuccessStatusCode();
            //    using (var stream = await response.Content.ReadAsStreamAsync())
            //    using (var streamReader = new StreamReader(stream))
            //    using (var jsonReader = new JsonTextReader(streamReader))
            //    {
            //        return ReadFully(jsonReader);

            //        //do some deserializing http://www.newtonsoft.com/json/help/html/Performance.htm
            //    }
            //}
            //public async Task<byte[]> GetReportAsync(string ReportName, List<Tuple<string, string>> Parametros, ExportFormat Format,string OtherSetting)
            //{

            //    string paramets= "";
            //    string ConfigRep = "&rs:Command=Render&rs:Format="+ GetExportFormatString(Format) +OtherSetting;
            //    foreach (Tuple<string, string> param in Parametros)
            //        paramets += "&"+param.Item1 + "=" + param.Item2;            
            //    string URL = _baseURL + ReportName + ConfigRep+ paramets;
            //    try
            //    {

            //        using (var handler = new HttpClientHandler { Credentials = UserNTL })
            //        using (var client = new HttpClient(handler))
            //        {
            //            var result = await client.GetAsync(_baseURL);
            //            if (result.IsSuccessStatusCode)
            //            {
            //                var Filebyte = result.Content.ReadAsStringAsync().Result;                       
            //                return Filebyte;
            //            }

            //            else { new ExceptionGeneral("respuesta del servidor Incorrecta !"); return null; }
            //        }                

            //    }            
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
    }
}
