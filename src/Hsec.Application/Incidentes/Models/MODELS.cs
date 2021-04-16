using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.Incidentes.Models
{
    /**
     * esta clase no se una
     * solo existe para que en el swagger se tenga una idea de los modelos a usar
     * 
     */
    public class MODELS 
    {
        public PlanVM PlanAccionVM { get; set; }
        public MaestroDataVM MaestroDataVM { get; set; }
        public IncidenteDto IncidenteDto { get; set; }
        /**
         * se resive en base64 UTF8
         */
        public CreateFileAtributesDto CreateFileAtributesDto { get; set; }


    }
}