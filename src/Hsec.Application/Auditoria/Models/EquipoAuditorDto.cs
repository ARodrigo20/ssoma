using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Auditoria;

namespace Hsec.Application.Auditoria.Models
{
  public class EquipoAuditorDto : IMapFrom<TEquipoAuditor>
  {
        public string CodAuditoria { get; set; }
        public long Correlativo { get; set; }
        public string CodTabla { get; set; }
        public int? NroEquipo { get; set; }
        public string CodPersona { get; set; }
        public string Lider { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EquipoAuditorDto, TEquipoAuditor>();
            profile.CreateMap<TEquipoAuditor, EquipoAuditorDto>();
        }
  }
}