using Hsec.Domain.Common;
using System;

namespace Hsec.Domain.Entities.General
{
    public class TJerarquiaPersona : AuditableEntity
    {
        public TJerarquiaPersona() {
            //Persona = new TPersona();
            Jerarquia = new TJerarquia();      
        }
        public int CodPosicion { get; set; }
        public string CodPersona { get; set; }
        public string CodElipse { get; set; }
        public int CodTipoPersona { get; set; }
        public string PosicionPrimaria { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }      
        //public TPersona Persona { get; set; }
        public TJerarquia Jerarquia { get; set; }
    }
}
