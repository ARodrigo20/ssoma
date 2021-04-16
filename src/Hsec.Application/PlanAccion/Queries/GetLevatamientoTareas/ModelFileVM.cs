using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.PlanAccion;

namespace Hsec.Application.PlanAccion.Queries.GetLevatamientoTareas
{
    public class ModelFileVM : IMapFrom<TFile>
    {
        public int correlativoArchivos { get; set; }
        public string nroDocReferencia { get; set; }
        public string nroSubDocReferencia { get; set; }
        public int grupoPertenece { get; set; }
        public string descripcion { get; set; }
        public string tipoArchivo { get; set; }
        public string nombre { get; set; }
        public long size { get; set; }
        public bool estado { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TFile, ModelFileVM>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));    
        }
    }
}

