using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Auditoria;

namespace Hsec.Application.Auditoria.Models
{
  public class ControlCriticoDto : IMapFrom<TAudCartilla>
  {
    public string CodAuditoria { get; set; }
    public string CodPeligroFatal { get; set; }
    public string CodCartilla { get; set; }
    public string Descripcion { get; set; }
    public ICollection<CCCriteriosDto> Criterios {get;set;}

    public ControlCriticoDto()
    {
      Criterios = new HashSet<CCCriteriosDto>();
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ControlCriticoDto, TAudCartilla>();
        profile.CreateMap<TAudCartilla, ControlCriticoDto>();
    }
    
  }
}