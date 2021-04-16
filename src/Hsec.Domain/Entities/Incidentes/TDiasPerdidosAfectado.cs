using Hsec.Domain.Common;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TDiasPerdidosAfectado : AuditableEntity
    {
        public string CodIncidente { get; set; }
        public int Correlativo { get; set; }
        public string CodTabla { get; set; }
        public string CodPersona { get; set; }
        public string CodTipAccidente { get; set; }
        public string PeridoAnio { get; set; }
        public string PeridoMes { get; set; }
        public string CantidadDias { get; set; }
        public string Tipo { get; set; }
        //public string CodInvestigaAfecado { get; set; }

        public virtual TIncidente CodIncidenteNavigation { get; set; }
    }
}
