using Hsec.Domain.Common;
using Hsec.Domain.Enums;
using System;

namespace Hsec.Domain.Entities.Otros
{
    public class TPersonaTolerancia : AuditableEntity
    {
        public string CodTolCero { get; set; }

        public string CodPersona { get; set; }

        public int Correlativo { get; set; }

        public TToleranciaCero ToleranciaCero { get; set; }

    }
}
