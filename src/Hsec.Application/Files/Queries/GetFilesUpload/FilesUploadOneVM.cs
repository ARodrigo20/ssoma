using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.PlanAccion;

namespace Hsec.Application.Files.Queries.GetFilesUpload
{
    public class FilesUploadOneVM : IMapFrom<TFile>
    {
        public int CorrelativoArchivos { get; set; }
        public string NroDocReferencia { get; set; }
        public string NroSubDocReferencia { get; set; }
        public string CodTablaRef { get; set; }
        public int GrupoPertenece { get; set; }
        public string Descripcion { get; set; }
        public string TipoArchivo { get; set; }
        public string Nombre { get; set; }
        public long Size { get; set; }
        public bool Estado { get; set; }
        public int? Revisado { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TFile, FilesUploadOneVM>();
            //.ForMember(i=>i.tarea,opt => opt.MapFrom(t => t.Tarea));    
        }
    }
}
