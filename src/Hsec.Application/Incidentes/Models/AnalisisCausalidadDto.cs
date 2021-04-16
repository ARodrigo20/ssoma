using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;

namespace Hsec.Application.Incidentes.Models
{
    public class AnalisisCausalidadDto : IMapFrom<TIncidenteAnalisisCausa>
    {
        public string CodCausa { get; set; }
        public string CodCondicion { get; set; }
        public string CodAnalisisCausa { get; set; }
        public string Comentario { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AnalisisCausalidadDto,TIncidenteAnalisisCausa>()
                .ForMember(t => t.CodAnalisis, opt => opt.MapFrom(t => t.CodAnalisisCausa));
                // .ForMember(t => t.Comentario, opt => opt.MapFrom(t => t.Descripcion));
            profile.CreateMap<TIncidenteAnalisisCausa,AnalisisCausalidadDto>()
                .ForMember(t => t.CodAnalisisCausa, opt => opt.MapFrom(t => t.CodAnalisis));
                // .ForMember(t => t.Descripcion, opt => opt.MapFrom(t => t.Comentario));
        }

        
    }
}