using System;
using System.Collections.Generic;
using System.Text;
using Hsec.Domain.Entities.YoAseguro;
using AutoMapper;
using Hsec.Application.Common.Mappings;

namespace Hsec.Application.YoAseguro.Models
{
    public class YoAseguroCallendarDto : IMapFrom<TYoAseguro>
    {
        public YoAseguroCallendarDto()
        {
        }
        public string Id { get; set; }
        public DateTime? Start { get; set; }
        public string Title { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TYoAseguro, YoAseguroCallendarDto>();
            profile.CreateMap<YoAseguroCallendarDto, TYoAseguro>()
                .ForMember(t => t.CodYoAseguro, opt => opt.MapFrom(t => t.Id))
                .ForMember(t => t.Fecha, opt => opt.MapFrom(t => t.Start))
                .ForMember(t => t.TituloReunion, opt => opt.MapFrom(t => t.Title));
        }
    }
}
