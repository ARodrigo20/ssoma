using Hsec.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;
using CorreoWS;
using Microsoft.Extensions.Configuration;

namespace Hsec.Infrastructure.Services
{
    public class CorreosService : ICorreosService
    {
        private readonly IConfiguration _config;
        private string UrlReporte { get; set; }
        //private WebServiceSoapClient CorreoW { get; set; }

        public CorreosService(IConfiguration configuration)
        {
            _config = configuration;
            UrlReporte = _config["appSettings:URLReporte"];            
        }

        public async Task NotificarAprobador(string emails, string DocReferencia,string CodTabla)
        {
            if (emails == "") return;
            var CorreoW = new WebServiceSoapClient(WebServiceSoapClient.EndpointConfiguration.WebServiceSoap);
            CorreoW.Enviar_Correo("HSEC_Web2", UrlReporte + "Correos/HSEC_Aprobacion", emails, null, null, "HSEC_Aprobaciones, Aprobación pendiente", "DocReferencia=" + DocReferencia + ";CodTabla=" + CodTabla);
        }

        public async Task NotificarDocAprobado(string emails, string DocReferencia, string CodTabla)
        {
            if (emails == "") return;
            var CorreoW = new WebServiceSoapClient(WebServiceSoapClient.EndpointConfiguration.WebServiceSoap);
            CorreoW.Enviar_Correo("HSEC_Web2", UrlReporte + "Correos/HSEC_DocAprobado", emails, null, null, "HSEC_Aprobaciones, Aprobación de documento", "DocReferencia=" + DocReferencia + ";CodTabla=" + CodTabla);
        }
       
        public async Task NotificarRechazo(string emails, string DocReferencia, string CodTabla)
        {
            if (emails == "") return;
            var CorreoW = new WebServiceSoapClient(WebServiceSoapClient.EndpointConfiguration.WebServiceSoap);
            CorreoW.Enviar_Correo("HSEC_Web2", UrlReporte + "Correos/HSEC_Rechazo", emails, null, null, "HSEC_Aprobaciones, Rechazo de documento", "DocReferencia=" + DocReferencia + ";CodTabla=" + CodTabla);
        }

        public async Task NotificarModificar(string emails, string DocReferencia, string CodTabla)
        {
            throw new NotImplementedException();
        }
    }
}
