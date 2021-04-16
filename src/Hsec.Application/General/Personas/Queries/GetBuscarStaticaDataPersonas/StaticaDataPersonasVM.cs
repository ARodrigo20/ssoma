using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities;
using Hsec.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hsec.Application.General.Personas.Queries.GetBuscarStaticaDataPersonas
{
    public class StaticaDataPersonasVM 
    {
        public IList<TipoDocumentoDto> ListTipoDocumentos { get; set; }
        public IList<TipoPersonaDto> ListTipoPersonas { get; set; }

    }
}
