using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.Observaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Observacion.Queries.GetObservacionesBuscar
{
    public class ObservacionesVM : IMapFrom<TObservacion>
    {
        public IList<ObservacionDto> Lists { get; set; }
        public int Count { get; set; }
    }
}
