using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Application.General.PlanAnual.Models;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
namespace Hsec.Application.General.PlanAnual.Commands.UpdatePlanAnual
{
    public class UpdatePlanAnualVM
    {
        public string Anio { get; set; }
        public string Mes { get; set; }
        public bool Replicar { get; set; }
        public ICollection<PersonaDto> ListPersonas { get; set; }

    }
}