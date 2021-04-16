using Hsec.Application.Common.Mappings;
using AutoMapper;
using Hsec.Domain.Entities.Inspecciones;

namespace Hsec.Application.Inspeccion.Models
{
    public class AtendidosDto : IMapFrom<TPersonaAtendida>
    {
        public string NroReferencia { get; set; }
        public string CodPersona { get; set; }
        public string Nombres { get; set; }
        public string Cargo { get; set; }
        public string NroDocumento { get; set; }
        public string Lider { get; set; }  //LIDER
        public string Estado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AtendidosDto, TPersonaAtendida>();
            profile.CreateMap<TPersonaAtendida, AtendidosDto>();
        }
    }
}