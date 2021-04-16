using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Capacitaciones.Preguntas.Command.Create.DTOs;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas.Command.Update.DTOs
{
    public class UpdatePreguntaDTO : IMapFrom<TPreguntas>
    {
        public UpdatePreguntaDTO()
        {
            alternativas = new List<AlternativasDTO>();
        }
        //public string codCurso { get; set; }
        public int codPregunta { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public double puntaje { get; set; }
        public string respuesta { get; set; }
        public IList<AlternativasDTO> alternativas { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TPreguntas, UpdatePreguntaDTO>();   
        }
    }
}