using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;
using System;

namespace Hsec.Application.Observacion.Queries.GetObservacionesBuscar
{
    public class ObservacionDto : IMapFrom<TObservacion>
    {
        public string CodObservacion { get; set; }
        public string CodPosicionGer { get; set; }
        public string CodPosicionSup { get; set; }
        public string CodAreaHsec { get; set; }
        public string CodTipoObservacion { get; set; }
        public string CodSubTipoObs { get; set; }
        public string CodNivelRiesgo { get; set; }
        public string CodObservadoPor { get; set; }
        public DateTime? FechaObservacion { get; set; }
        public string HoraObservacion { get; set; }
        public string CodUbicacion { get; set; } //?? 
        public string CodSubUbicacion { get; set; } //??
        public string CodUbicacionEspecifica { get; set; } //??
        public string DesUbicacion { get; set; }
        public string CodStopWork { get; set; }
        public string CodActoSubEstandar { get; set; }
        public string CodCondSubEstandar { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuModifica { get; set; }
        public bool Editable { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TObservacion, ObservacionDto>()
                .ForMember(d => d.FechaCreacion, opt => opt.MapFrom(s => s.Creado))
                .ForMember(d => d.UsuCreacion, opt => opt.MapFrom(s => s.CreadoPor))
                .ForMember(d => d.FechaModificacion, opt => opt.MapFrom(s => s.Modificado))
                .ForMember(d => d.UsuModifica, opt => opt.MapFrom(s => s.ModificadoPor));
            // profile.CreateMap<TodoItem, TodoItemDto>().ConvertUsing(value => { new TodoItemDto() });
        }

    }
}
