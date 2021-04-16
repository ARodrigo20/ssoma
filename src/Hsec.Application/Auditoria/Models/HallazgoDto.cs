using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Auditoria;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.Auditoria.Models
{
  public class HallazgoDto : IMapFrom<THallazgos>
  {
    public string CodHallazgo { get; set; }
    public string CodTipoHallazgo { get; set; }
    public NoConformidadDto NoConformidad { get; set; }
    public DatosHallazgoDto Observacion { get; set; }
    public DatosHallazgoDto OportunidadMejora { get; set; }
    public DatosHallazgoDto RequiereCorreccion { get; set; }
    
    public ICollection<PlanVM> planAccion { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<HallazgoDto, THallazgos>();
        profile.CreateMap<THallazgos, HallazgoDto>();
    }
  }
}