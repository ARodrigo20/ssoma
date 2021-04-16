using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Incidentes;

namespace Hsec.Application.Incidentes.Models
{
    public class ICAMDto : IMapFrom<TIcam>
    {
        public string CodTipoIcamfactOrg  { get; set; }
        public string FactOrg { get; set; }
        public string CodTipoCondEntIcam { get; set; }
        public string CondEnt { get; set; }
        public string CodTipoAccEquipIcam { get; set; }
        public string AccEquip { get; set; }
        public string CodTipoDefenAusen { get; set; }
        public string DefenAusen { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ICAMDto, TIcam>();
            profile.CreateMap<TIcam, ICAMDto>();
                //.ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority))
                //.ForMember(d => d.PriorityLevelDto, opt => opt.MapFrom(s => s.Priority));
            // profile.CreateMap<TodoItem, TodoItemDto>().ConvertUsing(value => { new TodoItemDto() });
        }

    }
}