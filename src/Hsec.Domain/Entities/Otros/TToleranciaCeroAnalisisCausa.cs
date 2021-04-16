using Hsec.Domain.Common;
using Hsec.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.Otros
{
    public class TToleranciaCeroAnalisisCausa : AuditableEntity
    {
        public string CodTolCero { get; set; }

        public string CodAnalisis { get; set; }

        public string Comentario { get; set; }

        public string CodCondicion { get; set; }

        public TToleranciaCero ToleranciaCero { get; set; }

    }
}
