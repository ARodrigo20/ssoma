using Hsec.Application.Common.Mappings;
using AutoMapper;
using Hsec.Domain.Entities.Inspecciones;

namespace Hsec.Application.Inspeccion.Models
{
    public class EquipoDto : IMapFrom<TEquipoInspeccion>
    {
        public string NroReferencia { get; set; }
        public string CodPersona { get; set; }
        public string Nombres { get; set; }
        public string Cargo { get; set; }
        public string NroDocumento { get; set; }
        public string Lider { get; set; }  //LIDER
        //public string Estado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EquipoDto, TEquipoInspeccion>();
            //.ForMember(t => t.CodInspeccion, opt => opt.MapFrom(t => t.NroReferencia))
            //.ForMember(t => t.CodPersona, opt => opt.MapFrom(t => t.CodPersona));
            profile.CreateMap<TEquipoInspeccion, EquipoDto>();
                //.ForMember(t => t.NroReferencia, opt => opt.MapFrom(t => t.CodInspeccion))
                //.ForMember(t => t.CodPersona, opt => opt.MapFrom(t => t.CodPersona));
        }
    }
}