using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;

namespace Hsec.Application.Observacion.Queries.GetObservacion
{
    public class CriterioDto : IMapFrom<TObsVCCRespuesta> 
    {
        public string CodCC { get; set; }
        public string CodCrit { get; set; }
        public string Respuesta { get; set; }
        public string Justificacion { get; set; }
        public string AccionCorrectiva { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TObsVCCRespuesta, CriterioDto>()
            .ForMember(t => t.CodCC, o => o.MapFrom(t => t.CodigoCC))
            .ForMember(t => t.CodCrit, otp => otp.MapFrom(t => t.CodigoCriterio) );

            profile.CreateMap<TObsVCCVerCCEfectividad, CriterioDto>();
        }
    }
}
