using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Queries.GetPersonasJerarquia
{
    public class AccionJerarquiaPersonaVM
    {
        public AccionJerarquiaPersonaVM()
        {
            listaCodPer = new List<AccionJerarquiaPersonaDto>();
        }
        public List<AccionJerarquiaPersonaDto> listaCodPer { get; set; }
        public int codPosicion { get; set; }
        public int pagina { get; set; }
        public int tamanio { get; set; }
    }
}
