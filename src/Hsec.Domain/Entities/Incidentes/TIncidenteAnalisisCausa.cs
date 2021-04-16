using Hsec.Domain.Common;
using System;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.Incidentes
{
    public class TIncidenteAnalisisCausa: AuditableEntity
    {
        // public int nro {get;set;}
        public string CodIncidente { get; set; }
        public string CodAnalisis { get; set; }
        public string Comentario { get; set; }
        public string CodCondicion { get; set; }
        public string CodCausa { get; set; }

        public virtual TIncidente CodIncidenteNavigation { get; set; }
    }
}
