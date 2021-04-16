using System;
using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Auditoria;

namespace Hsec.Application.Auditoria.Models
{
  public class AnalisisCausalidadDto : IMapFrom<TAuditoriaAnalisisCausalidad>
  {
    public string CodCausa { get; set; }
    public string CodCondicion { get; set; }
    public string CodAnalisisCausa { get; set; }
    public string DesCausa { get; set; }
    public string DesCondicion { get; set; }
    public string DesAnalisisCausa { get; set; }
    public string CodHallazgo { get; set; }
    public string Comentario { get; set; }
    public void Mapping(Profile profile)
    {
      profile.CreateMap<AnalisisCausalidadDto, TAuditoriaAnalisisCausalidad>()
        .ForMember(t => t.CodAnalisis ,otp => otp.MapFrom(t => t.CodAnalisisCausa));
        // .ForMember(t => t.CodCausa ,otp => otp.MapFrom(t => t.CodCausa));
      profile.CreateMap<TAuditoriaAnalisisCausalidad, AnalisisCausalidadDto>()
        .ForMember(t => t.CodAnalisisCausa ,otp => otp.MapFrom(t => t.CodAnalisis));
    }
  }
}