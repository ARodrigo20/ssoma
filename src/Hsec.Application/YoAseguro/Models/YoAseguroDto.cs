using System;
using System.Collections.Generic;
using System.Text;
using Hsec.Domain.Entities.YoAseguro;
using AutoMapper;
using Hsec.Application.Common.Mappings;

namespace Hsec.Application.YoAseguro.Models
{
    public class YoAseguroDto : IMapFrom<TYoAseguro>
    {
        public YoAseguroDto()
        {
            PersonasReconocidas = new HashSet<PersonaYoAseguroDto>();
        }
        public string CodYoAseguro { get; set; }
        public string CodPosGerencia { get; set; }
        public string CodPersonaResponsable { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? FechaEvalucion { get; set; }
        public int ReportadosObservaciones { get; set; }
        public int CorregidosObservaciones { get; set; }
        public string ObsCriticaDia { get; set; }
        public string Calificacion { get; set; }
        public string Comentario { get; set; }
        public string Reunion { get; set; }
        public string Recomendaciones { get; set; }
        public string TituloReunion { get; set; }
        public string TemaReunion { get; set; }
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }
        public ICollection<PersonaYoAseguroDto> PersonasReconocidas { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TYoAseguro, YoAseguroDto>();
            profile.CreateMap<YoAseguroDto, TYoAseguro>();
            //profile.CreateMap<YoAseguroDto, TYoAseguro>()
            //    .ForMember(t => t.TipoCartilla, opt => opt.MapFrom(t => "INTERACCION"));
        }
    }
}
