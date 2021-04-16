using System.Collections.Generic;

namespace Hsec.Application.General.PlanAnual.Models {
    public class PersonaDto {
        public string CodPersona { get; set; }
        public string Nombres {get;set;}
        public ICollection<PlanReferenciaDto> ListCodigos { get; set; }

        public PersonaDto(string codPersona, ICollection<PlanReferenciaDto> listCodigos)
        {
            CodPersona = codPersona;
            ListCodigos = listCodigos;
        }
        public PersonaDto () {
            ListCodigos = new HashSet<PlanReferenciaDto> ();
        }
    }
}