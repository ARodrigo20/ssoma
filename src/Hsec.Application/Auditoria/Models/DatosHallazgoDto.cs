using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Auditoria;

namespace Hsec.Application.Auditoria.Models
{
    public class DatosHallazgoDto : IMapFrom<TDatosHallazgo>
    {
        public int Correlativo { get; set; }
        public string CodHallazgo { get; set; }
        public string CodTipoHallazgo { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DatosHallazgoDto, TDatosHallazgo>();
            profile.CreateMap<TDatosHallazgo,DatosHallazgoDto>();
        }

    }
}