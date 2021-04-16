using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;

namespace Hsec.Application.Observacion.Commands.CreateObservacion
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
            profile.CreateMap<CriterioDto, TObsVCCRespuesta>()
            .ForMember(t => t.CodigoCC, o => o.MapFrom(t => t.CodCC))
            .ForMember(t => t.CodigoCriterio, o => o.MapFrom(t => t.CodCrit));

            profile.CreateMap<CriterioDto, TObsVCCVerCCEfectividad>();
        }
    }
}
