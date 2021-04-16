using System;
using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Auditoria;

namespace Hsec.Application.Auditoria.Models
{
  public class AuditoriaDto : IMapFrom<TAuditoria>
  {

    public AuditoriaDto(){
      Equipo = new HashSet<EquipoAuditorDto>();
      
      Hallazgos = new HashSet<HallazgoDto>();
    }
    public string CodAuditoria { get; set; }
    public string AuditoriaDescripcion { get; set; }
    public string CodPosicionGer { get; set; }
    public string CodPosicionSup { get; set; }
    public string CodContrata { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string CodRespAuditoria { get; set; }
    public string CodTipoAuditoria { get; set; }
    public string CodAreaAlcance { get; set; }
    public ICollection<EquipoAuditorDto> Equipo { get; set; }
    public ControlCriticoDto ControlCritico { get; set; }
    public ICollection<HallazgoDto> Hallazgos { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AuditoriaDto, TAuditoria>();
        profile.CreateMap<TAuditoria, AuditoriaDto>();
    }

  }
}
