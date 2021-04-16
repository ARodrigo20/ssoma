using System;
using System.Collections.Generic;
using System.Text;
using Hsec.Domain.Entities.YoAseguro;
using AutoMapper;
using Hsec.Application.Common.Mappings;

namespace Hsec.Application.YoAseguro.Queries.SearchYoAseguro
{
    public class SearchYoAseguroDto : IMapFrom<TYoAseguro>
    {
        public string Id { get; set; }
        public DateTime? Start { get; set; }
        public string Title { get; set; }
        public string Calificacion { get; set; }
        public string UsuCreacion { get; set; }
        public string UsuModifica { get; set; }
        public bool Editable { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TYoAseguro, SearchYoAseguroDto>()
                .ForMember(t => t.Id, opt => opt.MapFrom(t => t.CodYoAseguro))
                .ForMember(t => t.Start, opt => opt.MapFrom(t => t.FechaEvalucion))
                .ForMember(t => t.Title, opt => opt.MapFrom(t => t.Calificacion))
                .ForMember(t => t.UsuCreacion, opt => opt.MapFrom(t => t.CreadoPor))
                .ForMember(t => t.UsuModifica, opt => opt.MapFrom(t => t.ModificadoPor));
        }
    }
}
