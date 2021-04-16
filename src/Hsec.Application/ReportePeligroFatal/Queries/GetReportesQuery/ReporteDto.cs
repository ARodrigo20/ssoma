using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using System;

namespace Hsec.Application.ReportePeligroFatal.Queries.GetReportesQuery
{
    public class ReporteDto
    {
        public int Correlativo { get; set; }
        public string Posicion { get; set; }
        public string DesPosicion { get; set; }
        public string CodCC { get; set; }
        public string CodPeligro { get; set; }
        public string DesPeligro { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<TObservacionVerControlCritico, ReporteDto>()
        //        .ForMember(d => d.Posicion, opt => opt.MapFrom(s => s.CodPosicionGer));
        //}
    }
}
