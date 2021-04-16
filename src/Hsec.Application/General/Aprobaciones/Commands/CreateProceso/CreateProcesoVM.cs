using System.Collections.Generic;
using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities.General;

namespace Hsec.Application.General.Aprobaciones.Commands.CreateProceso
{
    public class CreateProcesoVM : IMapFrom<TProceso>
    {
        public string CodProceso { get; set; }
        public string Descripcion { get; set; }
        public IList<CreateCadenaAprobacionDto> Lista { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProcesoVM, TProceso>()
                .ForMember(t => t.CodProceso, opt => opt.Ignore());

        }
    }
}
