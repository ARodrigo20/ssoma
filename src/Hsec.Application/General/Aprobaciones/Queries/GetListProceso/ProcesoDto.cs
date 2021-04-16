using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Aprobaciones.Queries.GetListProceso
{
  public class ProcesoDto : IMapFrom<TProceso>
  {
    public string CodProceso { get; set; }
    public string Descripcion { get; set; }
    public void Mapping(Profile profile)
        {
            profile.CreateMap<TProceso,ProcesoDto>();
        }
  }
}
