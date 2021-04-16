using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using System.Collections.Generic;

namespace Hsec.Application.Incidentes.Models
{
    public class TestigosInvolucradosDetalleDto : IMapFrom<TTestigoInvolucrado>
    {
        public string CodPersona { get; set; }
        //public string Descripcion { get; set; }
        public string Grupo { get; set; }
        public string Manifestacion {get;set;}
        //public IList<ManifestacionDto> Manifestacion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TestigosInvolucradosDetalleDto, TTestigoInvolucrado>()
                .ForMember(t => t.NroGrupo, opt => opt.MapFrom(t => int.Parse(t.Grupo)))
                .ForMember(t => t.CodTabla, opt => opt.MapFrom(t => "testigo"));
            profile.CreateMap<TTestigoInvolucrado ,TestigosInvolucradosDetalleDto>()
                .ForMember(t => t.Grupo, opt => opt.MapFrom(t => t.NroGrupo.ToString()));
        }
    }
}