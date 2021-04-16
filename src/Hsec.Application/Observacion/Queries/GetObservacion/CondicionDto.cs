using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Queries.GetObservacion
{
    public class CondicionDto : IMapFrom<TObservacionCondicion>
    {
        public string CodObservacion { get; set; }
        public string CondObservada { get; set; }
        public string CondAccionInmediata { get; set; }
        public string CodActiRelacionada { get; set; }
        public string CodHha { get; set; }
        public string CodCondicionSubEstandar { get; set; }
        public string CodStopWork { get; set; }
        public string CodCorreccion { get; set; }
        public string CodSubTipo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TObservacionCondicion,CondicionDto>();
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }
    }
}
