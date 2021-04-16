using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.PlanAccion;

namespace Hsec.Application.PlanAccion.Commands.UpdatePlanDeAccion2
{
    public class ResposablesDtoUpdate : IMapFrom<TResponsable>
    {

        public ResposablesDtoUpdate() { }
        public int codAccion { get; set; }
        public string codPersona { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public double porcentajeMayor { get; set; }

        //public string CreadoPor { get; set; }
        //public DateTime Creado { get; set; }
        //public string ModificadoPor { get; set; }
        //public DateTime Modificado { get; set; }
        //public bool Estado { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ResposablesDtoUpdate, TResponsable>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));    
        }

    }
}
