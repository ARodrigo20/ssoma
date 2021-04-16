using Hsec.Domain.Entities.General;
using AutoMapper;
using Hsec.Application.Common.Mappings;

namespace Hsec.Application.General.Maestro.Models
{
    public class MaestroDto : IMapFrom<TMaestro>
    {
        public string CodTabla { get; set; }
        public string CodTipo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }
        public MaestroDto()
        {
        }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TMaestro, MaestroDto>();
            profile.CreateMap<MaestroDto, TMaestro>();
        }
    }
}