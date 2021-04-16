using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;

namespace Hsec.Application.Incidentes.Queries.GetCabeceraIncidente
{
    public class IncidenteCabeceraVM : IMapFrom<TIncidente>
    {
        //public string CodIncidente { get; set; }
        //public string CodTabla { get; set; }
        //public string CodTituloInci { get; set; }
        //public string DescripcionIncidente { get; set; }
        public string CodPosicionGer { get; set; }
        //public string CodRespGerencia { get; set; }
        public string CodPosicionSup { get; set; }
        //public string CodRespSuperint { get; set; }
        //public string CodProveedor { get; set; }
        //public string CodRespProveedor { get; set; }
        public string CodPerReporta { get; set; }
        public string CodAreaHsec { get; set; } //
        //public string HoraDelSuceso { get; set; }
        //public string CodTurno { get; set; }
        public string CodUbicacion { get; set; }
        public string CodSubUbicacion { get; set; }
        public string CodUbicacionEspecifica { get; set; }
        public string DesUbicacion { get; set; }
        public string CodTipoIncidente { get; set; }//
        public string CodSubTipoIncidente { get; set; }//
        public int? CodClasificaInci { get; set; } //
        public string CodClasiPotencial { get; set; } //
        //public string CodLesAper { get; set; }
        //public string CodHha { get; set; }
        //public string ResumenInfMedico { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TIncidente, IncidenteCabeceraVM>();
        }
    }
}