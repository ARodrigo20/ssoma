using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;
using System;

namespace Hsec.Application.Incidentes.Queries.GetBuscarInsidentes
{
    public class IncidenteBuscarDto : IMapFrom<TIncidente>
    {

        public long Correlativo { get; set; }
        public string CodIncidente { get; set; }
        public string CodTabla { get; set; }
        public string CodTituloInci { get; set; }
        public string DescripcionIncidente { get; set; }
        public string CodPosicionGer { get; set; }
        public string CodRespGerencia { get; set; }
        public string CodPosicionSup { get; set; }
        public string CodRespSuperint { get; set; }
        public string CodProveedor { get; set; }
        public string CodRespProveedor { get; set; }
        public string CodPerReporta { get; set; }
        public string CodAreaHsec { get; set; }
        public DateTime? FechaDelSuceso { get; set; }
        public string HoraDelSuceso { get; set; }
        public string CodTurno { get; set; }
        public string CodUbicacion { get; set; }
        public string CodSubUbicacion { get; set; }
        public string CodUbicacionEspecifica { get; set; }
        public string DesUbicacion { get; set; }
        public string CodTipoIncidente { get; set; }
        public string CodSubTipoIncidente { get; set; }
        public string CodClasificaInci { get; set; }
        public string CodClasiPotencial { get; set; }
        public string CodLesAper { get; set; }
        public string CodMedioAmbiente { get; set; }
        public string CodActiRelacionada { get; set; }
        public string CodHha { get; set; }
        public string ResumenInfMedico { get; set; }
        public string Conclusiones { get; set; }
        public string Aprendizajes { get; set; }
        public string Oculto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuModifica { get; set; }
        public bool Editable { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IncidenteBuscarDto, TIncidente>()
                //.ForMember(d => d.CodTipoIncidente, opt => opt.Condition((d,s)=>d.CodTipoIncidente!=null))
                //.ForMember(d => d.CodAreaHsec, opt => opt.Condition((a,b)=>b.Cod));
                ;
            profile.CreateMap<TIncidente, IncidenteBuscarDto>()
                .ForMember(d => d.FechaCreacion, opt => opt.MapFrom(s => s.Creado))
                .ForMember(d => d.UsuCreacion, opt => opt.MapFrom(s => s.CreadoPor))
                .ForMember(d => d.FechaModificacion, opt => opt.MapFrom(s => s.Modificado))
                .ForMember(d => d.UsuModifica, opt => opt.MapFrom(s => s.ModificadoPor));
        }
    }
}