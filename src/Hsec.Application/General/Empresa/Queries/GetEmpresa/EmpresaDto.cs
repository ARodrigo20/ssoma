using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Empresa.Queries.GetEmpresa
{
    public class EmpresaDto : IMapFrom<TProveedor>
    {
        public string CodProveedor { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TProveedor, EmpresaDto>()
                .ForMember(t => t.Descripcion, opt => opt.MapFrom(t => t.RazonSocial));

        }
    }
   

}