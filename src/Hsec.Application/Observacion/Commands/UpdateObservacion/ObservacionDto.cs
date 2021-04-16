using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.Observaciones;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.Files.Queries.GetFilesUpload;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion
{
    public class ObservacionDto : IMapFrom<TObservacion>
    {
        public ObservacionDto()
        {
            PlanAccion = new List<PlanVM>();
            FilesSTR = new List<FilesUploadOneVM>();
        }

        public string CodObservacion { get; set; }
        public string CodPosicionGer { get; set; }
        public string CodPosicionSup { get; set; }
        public string CodAreaHsec { get; set; }
        public string CodTipoObservacion { get; set; }
        public string CodSubTipoObs { get; set; }
        public string CodNivelRiesgo { get; set; }
        public string CodObservadoPor { get; set; }
        public DateTime? FechaObservacion { get; set; }
        public string HoraObservacion { get; set; }
        public string CodUbicacion { get; set; } //?? 
        public string CodSubUbicacion { get; set; } //??
        public string CodUbicacionEspecifica { get; set; } //??
        public string DesUbicacion { get; set; }

        public TareaDto? Tarea { get; set; }
        public CondicionDto? Condicion { get; set; }
        public ComportamientoDto? Comportamiento { get; set; }
        public IteraccionSeguridadDto? IteraccionSeguridad { get; set; }
        public VerificacionControlCriticoDto? VerificacionControlCritico { get; set; }
        public VerificacionControlCriticoDto? Covid19 { get; set; }

        public IFormFileCollection files { get; set; }
        public List<FilesUploadOneVM> FilesSTR { get; set; }

        public List<PlanVM> PlanAccion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ObservacionDto, TObservacion>();
            //profile.CreateMap<, CreateObservacionCommand > ();
        }
    }
}
