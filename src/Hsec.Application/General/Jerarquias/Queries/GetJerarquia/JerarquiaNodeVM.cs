using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Jerarquias.Queries.GetJerarquia
{
    public class JerarquiaNodeVM : IMapFrom<TJerarquia>
    {
        public JerarquiaNodeVM() {
            children = new List<JerarquiaNodeVM>();
        }
        public string label { get; set; }
        public int? data { get; set; }
        public string Tipo { get; set; }
        public string CodElipse { get; set; }
        public IList<JerarquiaNodeVM> children { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TJerarquia,JerarquiaNodeVM>()
                .ForMember(t => t.data , opt => opt.MapFrom(t => t.CodPosicion))
                .ForMember(t => t.label , opt => opt.MapFrom(t => t.Descripcion))
                .ForMember(t => t.Tipo , opt => opt.MapFrom(t => t.Tipo))
                .ForMember(t => t.CodElipse , opt => opt.MapFrom(t => t.CodElipse));
        }
    }
}