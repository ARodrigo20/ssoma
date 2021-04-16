using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Capacitaciones.Preguntas.Command.Create.DTOs;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas.Queries.Get.DTOs
{
    public class GetPreguntaDTO : IMapFrom<TPreguntas>
    {
        public GetPreguntaDTO()
        {
            alternativas = new List<AlternativasDTO>();
        }
        public string codCurso { get; set; }
        public int codPregunta { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public double puntaje { get; set; }
        public string respuesta { get; set; }
        public bool estado { get; set; }
        public int countRespuestas { get; set; }
        public IList<AlternativasDTO> alternativas { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TPreguntas, GetPreguntaDTO>()
            .ForMember(i=>i.estado,opt => opt.MapFrom(t => t.Estado));   
        }
    }
}
