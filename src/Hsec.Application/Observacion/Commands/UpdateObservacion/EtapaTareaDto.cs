using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Commands.UpdateObservacion
{
    public class EtapaTareaDto: IMapFrom<TObsTaEtapaTarea>
    {
        public string TituloEtapaTarea { get; set; }
        public string DescripcionEtapaTarea { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EtapaTareaDto,TObsTaEtapaTarea>();
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }
    }

}
