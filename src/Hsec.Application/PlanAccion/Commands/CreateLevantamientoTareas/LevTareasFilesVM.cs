using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.PlanAccion;
using System;
using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Commands.CreateLevantamientoTareas
{
    public class LevTareasFilesVM : IMapFrom<TLevantamientoPlan>
    {
        public LevTareasFilesVM()
        {
            files = new List<FilesDto>();
        }
        public int codAccion { get; set; }
        public string codPersona { get; set; }
        public int correlativo { get; set; }
        public string nombres { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        public DateTime? fecha { get; set; }
        public double porcentajeAvance { get; set; }
        public int size { get; set; }
        public bool Rechazado { get; set; }
        public IList<FilesDto> files { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TLevantamientoPlan, LevTareasFilesVM>();
        }
    }
}
