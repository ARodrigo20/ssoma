using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using System;

namespace Hsec.Application.Incidentes.Models
{
    public class DatosGeneralesDto : IMapFrom<TIncidente>
    {
        public string CodAreaHsec { get; set; }
        public string CodTipoIncidente { get; set; }
        public string CodSubTipoIncidente { get; set; }
        public string CodClasificaInci { get; set; }
        public string CodClasiPotencial { get; set; }
        public string CodLesAper { get; set; }
        public string CodActiRelacionada { get; set; }
        public string CodHha { get; set; }
        public DateTime FechaDelSuceso { get; set; }
        public string HoraDelSuceso { get; set; }
        public string CodTurno { get; set; }
        public string CodTituloInci { get; set; }
        public string DescripcionIncidente { get; set; }
        public string CodPosicionGer { get; set; }
        public string CodPosicionSup { get; set; }
        public string CodRespGerencia { get; set; }
        public string CodRespSuperint { get; set; }
        public string CodProveedor { get; set; }
        public string CodRespProveedor { get; set; }
        public string CodPerReporta { get; set; }
        public string CodUbicacion { get; set; }
        public string CodSubUbicacion { get; set; }
        public string CodUbicacionEspecifica { get; set; }
        public string DesUbicacion { get; set; }
        public string CodMedioAmbiente { get; set; }
        public string CodGrupoRiesgo { get; set; }
        public string CodRiesgo { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<DatosGeneralesDto, TIncidente>();
                //.ForMember(t => t.FechaDelSuceso, opt => opt.MapFrom(t => t.FechaDelSuceso.ToString() ));
            ;
            profile.CreateMap<TIncidente, DatosGeneralesDto>();
                //.ForMember(t => t.FechaDelSuceso, opt => opt.MapFrom(t => t.FechaDelSuceso.ToString()));


            //CodAreaHsec { get; set; }
            //CodTipoIncidente { get; set; }
            //CodSubTipoIncidente { get; set; }
            //CodClasificaInci { get; set; }
            //CodClasiPotencial { get; set; }
            //CodLesAper { get; set; }
            //CodActiRelacionada { get; set; }
            //CodHha { get; set; }
            //FechaDelSuceso { get; set; }
            //HoraDelSuceso { get; set; }
            //CodTurno { get; set; }
            //CodTituloInci { get; set; }
            //DescripcionIncidente { get; set; }
            //CodPosicionGer { get; set; }
            //CodPosicionSup { get; set; }
            //CodRespGerencia { get; set; }
            //CodRespSuperint { get; set; }
            //CodProveedor { get; set; }
            //CodRespProveedor { get; set; }
            //CodPerReporta { get; set; }
            //CodUbicacion { get; set; }
            //CodSubUbicacion { get; set; }
            //CodUbicacionEspecifica { get; set; }
            //DesUbicacion { get; set; }
            //CodMedioAmbiente { get; set; }

            // profile.CreateMap<TodoItem, TodoItemDto>().ConvertUsing(value => { new TodoItemDto() });
        }


    }
}