using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Queries.GetObservacion
{
    public class ComentarioDto : IMapFrom<TObsTaComentario>
    {
        public string CodTipoComentario { get; set; }
        public string Descripcion { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TObsTaComentario,ComentarioDto>();
            //.ForMember(m => m.Name, opt => opt.MapFrom(f => f.));
        }
    }
}
