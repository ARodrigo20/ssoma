using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Otros;

namespace Hsec.Application.Reunion.Models
{
    public class TAgendaDto : IMapFrom<TAgenda>
    {
        public string CodReunion { get; set; }

        public int Correlativo { get; set; }

        public string DesAgenda { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAgendaDto, TAgenda>();
            profile.CreateMap<TAgenda, TAgendaDto>();
        }
    }
}
