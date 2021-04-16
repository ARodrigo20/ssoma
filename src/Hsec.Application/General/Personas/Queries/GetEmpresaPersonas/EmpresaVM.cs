using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Personas.Queries.GetEmpresaPersonas
{
    public class EmpresaVM : IMapFrom<TPersona>
    {
        public int Correlativo { get; set; }
        public string Empresa { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TPersona, EmpresaVM>()
                .ForMember(e => e.Empresa, opt => opt.MapFrom(s => s.Empresa));
        }
    }
}