using System;

namespace Hsec.Application.PlanAccion.Queries.GetPersonasJerarquia
{
    public class GetAccionPersonaJerarquiaDto
    {
        public string? Tarea { get; set; }
        public DateTime FechaFinal { get; set; }
        //public string EstadoPlan { get; set; }
        public string CodEstadoAccion { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string? DocReferencia { get; set; }
        public int CodAccion { get; set; }
        public string CodTablaRef { get; set; }
    }
}
