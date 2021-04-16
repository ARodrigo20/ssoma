using AutoMapper;
using Hsec.Application.Comite.Models;
using Hsec.Application.Common.Mappings;
using Hsec.Application.Common.Models;
using Hsec.Domain.Entities.Otros;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;

namespace Hsec.Application.Comite.Command.ComiteCreate
{
    public class CreateComiteDto : IMapFrom<TComite>
    {
        public string CodComite { get; set; }

        public DateTime Fecha { get; set; }

        public string Hora { get; set; }

        public string CodTipoComite { get; set; }

        public string CodCategoria { get; set; }

        public string CodPosicionGer { get; set; }

        public string CodPosicionSup { get; set; }

        public string Lugar { get; set; }

        public string DetalleApertura { get; set; }

        public string CodSecretario { get; set; }

        public string ResumenSalud { get; set; }

        public string ResumenSeguridad { get; set; }

        public string ResumenMedioAmbiente { get; set; }

        public string ResumenComunidad { get; set; }

        public DateTime FechaCierre { get; set; }

        public string HoraCierre { get; set; }

        public ICollection<string> ListaParticipantes { get; set; }

        public List<PlanVM> PlanAccion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateComiteDto, ComiteDto>();
            profile.CreateMap<ComiteDto, CreateComiteDto>();
        }
    }
}
