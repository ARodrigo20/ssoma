using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Auditoria;

namespace Hsec.Application.Auditoria.Models
{
  public class CCCriteriosDto : IMapFrom<TAudCCCriterio>
  {
    public string CodAuditoria { get; set; }
    public string CodCartilla { get; set; }
    public string CodCC { get; set; }
    public string CodCriterio { get; set; }
    public float Resultado { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CCCriteriosDto, TAudCCCriterio>();
        profile.CreateMap<TAudCCCriterio, CCCriteriosDto>();
    }
  }
}