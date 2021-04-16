using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Jerarquias.Queries.GetJerarquia
{
    public class SupervisorVM : IMapFrom<TJerarquia>
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TJerarquia, SupervisorVM>()
                .ForMember(t => t.codigo, opt => opt.MapFrom(t => t.CodPosicion))
                .ForMember(t => t.descripcion, opt => opt.MapFrom(t => t.Descripcion));
        }
    }
}