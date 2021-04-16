using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Otros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.ToleranciaCero.Models
{
    public class ToleranciaCeroDto : IMapFrom<TToleranciaCero>
    {
        public string CodTolCero { get; set; }

        public DateTime FechaTolerancia { get; set; }

        public string CodPosicionGer { get; set; }

        public string CodPosicionSup { get; set; }

        public string Proveedor { get; set; }

        public string AntTolerancia { get; set; }

        public string IncumpDesc { get; set; }

        public string ConsecReales { get; set; }

        public string ConsecPot { get; set; }

        public string ConclusionesTol { get; set; }

        public string CodDetSancion { get; set; }

        public ICollection<TPersonaToleranciaDto> ToleranciaPersonas { get; set; }

        public ICollection<TToleranciaCeroAnalisisCausaDto> ToleranciaAnalisisCausa { get; set; }

        public ICollection<TRegTolDetalleDto> ToleranciaReglas { get; set; }

        public ToleranciaCeroDto()
        {
            ToleranciaPersonas = new HashSet<TPersonaToleranciaDto>();
            ToleranciaAnalisisCausa = new HashSet<TToleranciaCeroAnalisisCausaDto>();
            ToleranciaReglas = new HashSet<TRegTolDetalleDto>();
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ToleranciaCeroDto, TToleranciaCero>();
            profile.CreateMap<TToleranciaCero, ToleranciaCeroDto>();
        }

    }
}
