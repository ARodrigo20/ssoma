using Hsec.Domain.Common;
using Hsec.Domain.Enums;
using System;

namespace Hsec.Domain.Entities.Otros
{
    public class TRegTolDetalle : AuditableEntity
    {

        public string CodRegla { get; set; }

        public string CodTolCero { get; set; }

        public TToleranciaCero ToleranciaCero { get; set; }


    }
}
