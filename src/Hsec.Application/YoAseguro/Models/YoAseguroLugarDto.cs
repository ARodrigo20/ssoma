using System;
using Hsec.Domain.Entities.YoAseguro;
using AutoMapper;
using Hsec.Application.Common.Mappings;

namespace Hsec.Application.YoAseguro.Models
{
    public class YoAseguroLugarDto : IMapFrom<TYoAseguroLugar>
    {
        public string CodUbicacion { get; set; }
        //public string CodUbicacionPadre { get; set; }
        public string Descripcion { get; set; }
        //public int DiasLaborados { get; set; }
        //public int DiasDescanso { get; set; }
        //public DateTime? FechaReferencia { get; set; }
        public YoAseguroLugarDto()
        {
        }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TYoAseguroLugar, YoAseguroLugarDto>();
            profile.CreateMap<YoAseguroLugarDto, TYoAseguroLugar>();
        }
    }
}
