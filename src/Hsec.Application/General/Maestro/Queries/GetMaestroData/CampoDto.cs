using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Maestro.Queries.GetMaestroData
{
    public class CampoDto : IMapFrom<TMaestro> 
    {
      //  public string CodTabla { get; set; }
        public string CodRegistro { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TMaestro, CampoDto>()
                .ForMember(t => t.CodRegistro, opt => opt.MapFrom(t => t.CodTipo));
            profile.CreateMap<TAnalisisCausa, CampoDto>()
               // .ForMember(t => t.CodTabla, opt => opt.MapFrom(t => t.CodCondicion))
                .ForMember(t => t.CodRegistro, opt => opt.MapFrom(t => t.CodAnalisis))
                .ForMember(t => t.Descripcion, opt => opt.MapFrom(t => t.Descripcion));
        }
    }
}