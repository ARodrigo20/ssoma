using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Capacitaciones
{
    public class TRiesgo : AuditableEntity
    {
        //public Triesgo()
        //{
        //    TcontrolCritico = new HashSet<TcontrolCritico>();
        //}
        public string CodRiesgo { get; set; }
        public string CodPeligro { get; set; }
        public string Descripcion { get; set; }
        //public virtual ICollection<TcontrolCritico> TcontrolCritico { get; set; }
    }
        
}
