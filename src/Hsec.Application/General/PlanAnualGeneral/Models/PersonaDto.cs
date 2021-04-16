using System.Collections.Generic;

namespace Hsec.Application.General.PlanAnualGeneral.Models {
    public class PersonaDto {
        public string CodPersona { get; set; }
        public string Nombres {get;set;}
        public IEnumerable<PlanReferenciaDto> ListCodigos { get; set; }

        public PersonaDto(string codPersona, IEnumerable<PlanReferenciaDto> listCodigos)
        {
            CodPersona = codPersona;
            ListCodigos = listCodigos;
        }
        public PersonaDto () {
            ListCodigos = new List<PlanReferenciaDto> ();
        }
    }
}