using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Enums;

namespace Hsec.Application.General.Personas.Queries.GetPersonaByNomApeNroAll
{
    public class PersonaDto : IMapFrom<TPersona>
    {
        public string CodPersona { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Empresa { get; set; }
        public string CodCargo { get; set; }
        public string FlagPersistente { get; set; }
        //public TipoPersona CodTipoPersona { get; set; }
        public string CodTipoPersona { get; set; }
        //public TipoDocumento CodTipoDocumento { get; set; }
        public string CodTipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Sexo { get; set; }

        public string TipoDocumento { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TPersona, PersonaDto>()
                .ForMember(p => p.TipoDocumento, opt => opt.MapFrom(s => s.CodTipDocIden));
            //.ForMember(p => p.NroDocumento, opt => opt.MapFrom(s => s.TipoDocPersonas.ToList().FirstOrDefault().NroDocumento));
        }
    }
}