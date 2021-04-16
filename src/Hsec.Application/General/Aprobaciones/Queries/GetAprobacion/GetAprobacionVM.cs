using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;
namespace Hsec.Application.General.Aprobaciones.Queries.GetAprobacion
{
    public class GetAprobacionVM : IMapFrom<TAprobacion>
    {
        
        public string DocReferencia { get; set; }
        public string ProcesoAprobacion { get; set; }
        // public string CodProcesoAprobacion { get; set; }
        // public string CadenaAprobacion { get; set; }
        public string Version { get; set; }
        public string EstadoDoc { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TAprobacion,GetAprobacionVM>();
        }
    }
}
