using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Enums;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion
{
    public class CierreInteraccionDto : IMapFrom<TObsVCCCierreIteraccion>  
    {
        public string Codigo { get; set; }
        public string Respuesta { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CierreInteraccionDto, TObsVCCCierreIteraccion>()
            .ForMember(t => t.CodDesCierreIter , opt => opt.MapFrom(t => t.Codigo));
        }
    }
}