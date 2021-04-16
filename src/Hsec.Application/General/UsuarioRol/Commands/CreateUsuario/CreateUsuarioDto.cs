using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
using System;

namespace Hsec.Application.General.UsuarioRol.Commands.CreateUsuario
{
    public class CreateUsuarioDto  : IMapFrom<TUsuario>
    {
        public string codUsuario { get; set; }
        public string usuario { get; set; }
        public string ruc { get; set; }
        public string password { get; set; }
        public string rePassword { get; set; }
        public string rol { get; set; }
        public string tipoUsuario { get; set; }
        public bool tipoLogueo { get; set; }
        public string codPersona { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUsuarioDto, TUsuario>()
                .ForMember(t => t.CodUsuario, opt => opt.MapFrom( t => Int32.Parse(t.codUsuario) ) );
            profile.CreateMap<TUsuario, CreateUsuarioDto>();
        }

    }
}
