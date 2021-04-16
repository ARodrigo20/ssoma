using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Capacitaciones;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs
{
    public class GetCursoProgramacionExpositorDto : IMapFrom<TExpositor>
    {
        public string codPersona { get; set; }
        public string codCurso { get; set; }
        public string nombre { get; set; }
        public bool tipo { get; set; } // 1 -> interno y 0 -> externo
        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<TExpositor, GetCursoProgramacionExpositorDto>()
        //    .ForMember(i=>i.codTemaCapacita,opt => opt.MapFrom(t => t.CodCurso));    
        //}   
    }
}
