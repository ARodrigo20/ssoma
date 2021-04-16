using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.Otros;
using System;
using System.Collections.Generic;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.Reunion.Models
{
    public class TReunionDto : IMapFrom<TReunion>
    {
        public string CodReunion { get; set; }

        public string Reunion { get; set; }

        public string Lugar { get; set; }

        public DateTime Fecha { get; set; }

        public string Hora { get; set; }

        public string CodPerFacilitador { get; set; }

        public string Acuerdos { get; set; }

        public string Comentarios { get; set; }

        public string Otros { get; set; }

        public ICollection<TAsistentesReunionDto> ReunionAsistentes { get; set; }

        public ICollection<TAusentesReunionDto> ReunionAusentes { get; set; }

        public ICollection<TJustificadosReunionDto> ReunionJustificados { get; set; }

        public ICollection<TAgendaDto> ReunionAgendas { get; set; }

        public List<PlanVM> PlanAccion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TReunionDto, TReunion>();
            profile.CreateMap<TReunion, TReunionDto>();
        }

        public TReunionDto()
        {
            ReunionAsistentes = new HashSet<TAsistentesReunionDto>();
            ReunionAusentes = new HashSet<TAusentesReunionDto>();
            ReunionJustificados = new HashSet<TJustificadosReunionDto>();
            ReunionAgendas = new HashSet<TAgendaDto>();
        }
    }
}
