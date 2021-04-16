using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Aprobaciones.Queries.GetProceso
{
    public class GetProcesoVM : IMapFrom<TProceso>
    {
        public string CodProceso { get; set; }
        public string Descripcion { get; set; }
        public IList<GetCadenaAprobacionDto> Lista { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TProceso, GetProcesoVM>();
        }
    }
}

