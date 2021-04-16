using Hsec.Application.Common.Mappings;
using AutoMapper;
using Hsec.Domain.Entities.Inspecciones;

namespace Hsec.Application.Inspeccion.Models
{
    public class InspeccionAnalisisCausaDto : IMapFrom<TInspeccionAnalisisCausa>
    {
        public long Correlativo { get; set; }
        public string CodInspeccion { get; set; }
        public string CodAnalisis { get; set; }
        public string CodCausa { get; set; }
        public string CodCondicion { get; set; }
        public string Descripcion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InspeccionAnalisisCausaDto, TInspeccionAnalisisCausa>();
            profile.CreateMap<TInspeccionAnalisisCausa, InspeccionAnalisisCausaDto>();
        }
    }
}