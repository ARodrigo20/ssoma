using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Otros;

namespace Hsec.Application.Comite.Models
{
    public class TListaParticipantesComiteDto : IMapFrom<TListaParticipantesComite>
    {
        public int Correlativo { get; set; }

        public string CodComite { get; set; }

        public string CodPersona { get; set; }

        public int CodTipDocIden { get; set; }

        public string Lider { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TListaParticipantesComiteDto, TListaParticipantesComite>();
            profile.CreateMap<TListaParticipantesComite, TListaParticipantesComiteDto>();
        }
    }
}
