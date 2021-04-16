using System;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class GetPlanAccionFiltradoDto
    {
        public int pagina { get; set; }
        public int tamanio { get; set; }
        public int codAccion { get; set; }
        public string docReferencia { get; set; }
        public string codTablaRef { get; set; }
        public int? codPosicion { get; set; }    
        public string codEstadoAccion { get; set; }
        public string codEstadoApro { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }
        public string codPersonaForm { get; set; }
        public string codSolicitadoPor { get; set; }
        public string codPersona { get; set; }
        public bool check { get; set; }
    }
}
