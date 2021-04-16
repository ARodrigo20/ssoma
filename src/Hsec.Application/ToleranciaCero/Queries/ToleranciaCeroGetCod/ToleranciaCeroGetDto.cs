using Hsec.Application.Common.Mappings;
using Hsec.Application.ToleranciaCero.Command.ToleranciaCeroInsert;
using Hsec.Application.ToleranciaCero.Models;
using Hsec.Domain.Entities.Otros;
using System;
using System.Collections.Generic;
using System.Text;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.ToleranciaCero.Queries.ToleranciaCeroGetCod
{
    public class ToleranciaCeroGetDto : IMapFrom<TToleranciaCero>
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

        //public ICollection<PersonaDto> Personas { get; set; }
        public ICollection<PersonaVM> Personas { get; set; }

        public ICollection<TToleranciaCeroAnalisisCausaDto> Causas { get; set; }

        public ICollection<string> Reglas { get; set; }

        public ToleranciaCeroGetDto()
        {
            Causas = new HashSet<TToleranciaCeroAnalisisCausaDto>();
            Personas = new HashSet<PersonaVM>();
        }
    }
}
