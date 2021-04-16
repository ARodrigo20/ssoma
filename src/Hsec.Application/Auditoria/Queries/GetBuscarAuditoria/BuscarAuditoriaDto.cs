using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Auditoria;
using Hsec.Domain.Enums;
namespace Hsec.Application.Incidentes.Queries.GetBuscarInsidentes
{
  public class BuscarAuditoriaDto : IMapFrom<TAuditoria>
  {
    public string CodAuditoria { get; set; }
    public string AuditoriaDescripcion { get; set; }
    public string CodTipoAuditoria { get; set; }
    public string FechaInicio { get; set; }
    public string FechaFin { get; set; }
    public string UsuarioCreado { get; set; }
    public string UsuarioModifica { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TAuditoria,BuscarAuditoriaDto>();
    }
  }
}

