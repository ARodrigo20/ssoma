using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System.Collections.Generic;

namespace Hsec.Application.General.AnalisisCausas.Queries.GetAnalisisCausas
{
  
    public class AnalisisCausaDto : IMapFrom<TAnalisisCausa>
    {
        public AnalisisCausaDto()
        {
            children = new List<AnalisisCausaDto>();
        }
        public string label { get; set; }
        public string data { get; set; }
        public int? nivel { get; set; }
        public IList<AnalisisCausaDto> children { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAnalisisCausa, AnalisisCausaDto>()
                .ForMember(t => t.data, opt => opt.MapFrom(t => t.CodAnalisis))
                .ForMember(t => t.label, opt => opt.MapFrom(t => t.Descripcion))
                .ForMember(t => t.nivel, opt => opt.MapFrom(t => t.Nivel));
        }
    }
}
