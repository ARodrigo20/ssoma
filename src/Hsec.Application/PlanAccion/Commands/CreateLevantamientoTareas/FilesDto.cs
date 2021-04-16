using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.PlanAccion;

namespace Hsec.Application.PlanAccion.Commands.CreateLevantamientoTareas
{
    public class FilesDto : IMapFrom<TFile>
    {
        public int correlativoArchivos { get; set; }
        public double size { get; set; }
        public string descripcion { get; set; }
        public int? grupoPertenece { get; set; }
        public string nombre { get; set; }
        public string nroSubDocReferencia { get; set; }
        public string tipoArchivo { get; set; }
        public bool estado { get; set; }
        public string nroDocReferencia { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TFile, FilesDto>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));            
        }
    }
}
