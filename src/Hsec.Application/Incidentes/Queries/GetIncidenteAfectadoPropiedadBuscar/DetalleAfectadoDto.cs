using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;

namespace Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPropiedadBuscar
{
    public class DetalleAfectadoDto : IMapFrom<TIncidente>
    {
        public string TipoAfectado { get; set; }
        public string Operador {get;set;}
        public string PropDescripcion {get;set;}
        public string Costo {get;set;}
        public string Monto {get;set;}
        public string TipoActivo {get;set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAfectadoPropiedad, DetalleAfectadoDto>()
                .ForMember(d => d.TipoAfectado, opt => opt.MapFrom(t => Domain.Enums.TipoAfectado.Propiedad.ToString()))
                .ForMember(d => d.Operador, opt => opt.MapFrom(pro => pro.Operador))
                .ForMember(d => d.PropDescripcion, opt => opt.MapFrom(pro => pro.Descripcion))
                .ForMember(d => d.Costo, opt => opt.MapFrom(pro => pro.CodCosto))
                .ForMember(d => d.Monto, opt => opt.MapFrom(pro => pro.Monto))
                .ForMember(d => d.TipoActivo, opt => opt.MapFrom(pro => pro.CodTipActivo));
        }
    }
}