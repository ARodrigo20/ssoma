using Hsec.Domain.Common;
using Hsec.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.Otros
{
    public class TToleranciaCero : AuditableEntity
    {
        public string CodTolCero { get; set; }

        public DateTime FechaTolerancia { get; set; }

        public string CodPosicionGer { get; set; }

        public string CodPosicionSup { get; set; }

        public string Proveedor { get; set; }

        public string AntTolerancia{ get; set; }

        public string IncumpDesc { get; set; }

        public string ConsecReales { get; set; }

        public string ConsecPot { get; set; }

        public string ConclusionesTol { get; set; }

        public string CodDetSancion { get; set; }

        public ICollection<TPersonaTolerancia>  ToleranciaPersonas{ get; set; }

        public ICollection<TToleranciaCeroAnalisisCausa> ToleranciaAnalisisCausa { get; set; }

        public ICollection<TRegTolDetalle> ToleranciaReglas { get; set; }

        public TToleranciaCero()
        {
            ToleranciaPersonas = new HashSet<TPersonaTolerancia>();
            ToleranciaAnalisisCausa = new HashSet<TToleranciaCeroAnalisisCausa>();
            ToleranciaReglas = new HashSet<TRegTolDetalle>();
        }

    }
}
