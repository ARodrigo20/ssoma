using Hsec.Domain.Entities.General;
using AutoMapper;
using Hsec.Application.Common.Mappings;

namespace Hsec.Application.General.PlanAnualGeneral.Models
{
    public class ModuloDto : IMapFrom<TModulo>
    {
        public string CodModulo { get; set; }
        public string CodModuloPadre { get; set; }
        public string Descripcion { get; set; }
        public ModuloDto()
        {
        }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TModulo, ModuloDto>();
            profile.CreateMap<ModuloDto, TModulo>();
        }
    }
}
