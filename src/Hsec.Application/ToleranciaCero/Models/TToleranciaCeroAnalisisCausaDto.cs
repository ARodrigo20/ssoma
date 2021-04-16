using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Otros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.ToleranciaCero.Models
{
    public class TToleranciaCeroAnalisisCausaDto : IMapFrom<TToleranciaCeroAnalisisCausa>
    {
        public string CodTolCero { get; set; }

        public string CodAnalisis { get; set; }

        public string Comentario { get; set; }

        public string CodCondicion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TToleranciaCeroAnalisisCausaDto, TToleranciaCeroAnalisisCausa>();
            profile.CreateMap<TToleranciaCeroAnalisisCausa, TToleranciaCeroAnalisisCausaDto>();
        }
    }
}
