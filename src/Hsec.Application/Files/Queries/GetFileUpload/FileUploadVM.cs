using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.PlanAccion;

namespace Hsec.Application.Files.Queries.GetFileUpload
{
    public class FileUploadVM : IMapFrom<TFile>
    {
        public int CorrelativoArchivos { get; set; }
        public string NroDocReferencia { get; set; }
        public string NroSubDocReferencia { get; set; }
        public int GrupoPertenece { get; set; }
        public byte[] ArchivoData { get; set; }
        public string Descripcion { get; set; }
        public string TipoArchivo { get; set; }
        public string Nombre { get; set; }
        public string CodTablaRef { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TFile, FileUploadVM>();
        }
    }
}
