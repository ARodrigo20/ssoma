using System.Collections.Generic;

namespace Hsec.Application.General.PlanAnualVerifiacionCC.Queries.GetPlanAnualSeguimiento
{
  public class PersonaPSADto
  {
    public string CodPersona { get; set; }
    public string Nombres { get; set; }
    public ICollection<ModuloEjeDto> ListModulo { get; set; }

    public PersonaPSADto(string codPersona, ICollection<ModuloEjeDto> ListModulo)
    {
        this.CodPersona = codPersona;
        this.ListModulo = ListModulo;
    }
    public PersonaPSADto () {
        ListModulo = new HashSet<ModuloEjeDto> ();
    }
  }
}