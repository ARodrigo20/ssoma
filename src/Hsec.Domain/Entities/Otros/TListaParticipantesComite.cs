using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Otros
{
    public class TListaParticipantesComite : AuditableEntity
    {
        public int Correlativo { get; set; }

        public string CodComite { get; set; }

        public string CodPersona { get; set; }

        public int CodTipDocIden { get; set; }

        public TComite Comite { get; set; }
    }
}
