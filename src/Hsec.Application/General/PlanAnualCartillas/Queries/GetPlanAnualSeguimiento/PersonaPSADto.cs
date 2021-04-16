using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.PlanAnualCartillas.Queries.GetPlanAnualSeguimiento
{
    public class PersonaPSADto
    {
        public string CodPersona { get; set; }
        public string Nombres { get; set; }
        public ICollection<CartillaEjeDto> ListCartilla { get; set; }
        public PersonaPSADto(string codPersona, ICollection<CartillaEjeDto> ListCartilla)
        {
            this.CodPersona = codPersona;
            this.ListCartilla = ListCartilla;
        }
        public PersonaPSADto()
        {
            ListCartilla = new HashSet<CartillaEjeDto>();
        }
    }
}