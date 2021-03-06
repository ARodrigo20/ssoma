using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion
{
    public class TareaDto : IMapFrom<TObservacionTarea>
    {
        public TareaDto()
        {
            PersonaObservadas = new List<string>();
            RegistroEncuestas = new List<RegistroEncuestaDto>();
            EtapaTareas = new List<EtapaTareaDto>();
            Comentarios = new List<ComentarioDto>();
        }
        public string CodObservacion { get; set; }
        public string CodTabla { get; set; }
        public string TareaObservada { get; set; }
        public string Pet { get; set; }
        public string CodActiRelacionada { get; set; }
        public string CodHha { get; set; }
        public string CodErrorObs { get; set; }
        public string CodEstadoObs { get; set; }
        // public string DesConclusiones { get; set; }
        // public string DesConclusionesfeedback { get; set; }
        // public string DesConclusionesProModi { get; set; }
        public string ComenRecoOpor { get; set; }
        public string CodStopWork { get; set; }

        public IList<string> PersonaObservadas { get; set; }
        public IList<RegistroEncuestaDto> RegistroEncuestas { get; set; }
        public IList<EtapaTareaDto> EtapaTareas { get; set; }
        public IList<ComentarioDto> Comentarios { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TareaDto,TObservacionTarea>()
                .ForMember(t => t.CodObservacion, opt => opt.Ignore());
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }

    }
}
