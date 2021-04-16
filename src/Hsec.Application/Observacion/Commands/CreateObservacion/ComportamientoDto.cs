using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Commands.CreateObservacion
{
    public class ComportamientoDto : IMapFrom<TObservacionComportamiento>
    {
        public string CodObservacion { get; set; }
        public string CompObservada { get; set; }
        public string CompAccionInmediata { get; set; }
        public string CodActiRelacionada { get; set; }
        public string CodEstado { get; set; }
        public string CodHha { get; set; }
        public string CodErrorObs { get; set; }
        public string CodActoSubEstandar { get; set; }
        public string CodStopWork { get; set; }
        public string CodCorreccion { get; set; }
        //public string CodEPP { get; set; }
        //public string CodTipoProteccion { get; set; }
        public string CodSubTipo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ComportamientoDto,TObservacionComportamiento>();
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }
    }
}
