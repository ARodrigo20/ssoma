using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System;

namespace Hsec.Application.General.Personas.Queries.GetPersona
{
    public class PersonaVM : IMapFrom<TPersona>
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
        public string Cargo { get; set; }
        public DateTime FechaIncicio { get; set; }

        public string TipoDocumento { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TPersona, PersonaVM>()
                .ForMember(p => p.Cargo, opt => opt.MapFrom(s => s.Ocupacion))
                .ForMember(p => p.FechaIncicio, opt => opt.MapFrom(s => s.Creado))
                .ForMember(p => p.TipoDocumento, opt => opt.MapFrom(s => s.CodTipDocIden));
                //.ForMember(p => p.NroDocumento, opt => opt.MapFrom(s => s.TipoDocPersonas.ToList().FirstOrDefault().NroDocumento));
        }
    }
}
