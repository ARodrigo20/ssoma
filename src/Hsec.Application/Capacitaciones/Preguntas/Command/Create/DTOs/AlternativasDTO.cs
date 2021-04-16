using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Preguntas.Command.Create.DTOs
{
    public class AlternativasDTO : IMapFrom<TAlternativas>
    {
        public int codAlternativa { get; set; }
        public int codPregunta { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAlternativas, AlternativasDTO>();   
        }
    }
}
