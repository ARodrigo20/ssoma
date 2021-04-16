using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.PlanAccion;

namespace Hsec.Application.PlanAccion.Queries.GetPlanDeAccion
{
    public class ResponsablesDto : IMapFrom<TResponsable>
    {
        public ResponsablesDto() { }
        public int codAccion { get; set; }
        public string codPersona { get; set; }
        public string nombres { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public double porcentajeMayor { get; set; }
        public bool estado { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TResponsable, ResponsablesDto>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));    
        }
    }
}
