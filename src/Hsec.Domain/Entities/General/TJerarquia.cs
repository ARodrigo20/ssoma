using Hsec.Domain.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hsec.Domain.Entities.General
{
    public class TJerarquia : AuditableEntity
    {
        public TJerarquia() {
            Hijos = new HashSet<TJerarquia>();
            JerarquiaPersona = new HashSet<TJerarquiaPersona>();            
        }
        public int? CodPosicion { get; set; }        
        public int? CodPosicionPadre { get; set; }
        public TJerarquia Padre { get; set; }
        public string CodElipse { get; set; }
        public string CodElipsePadre { get; set; }
        public string Descripcion { get; set; }
        public string Cargo { get; set; }
        //public bool Estado { get; set; }
        public bool Visible { get; set; }
        public string PathJerarquia { get; set; }
        //public HierarchyId Jerarquia { get; set; }
        public short? NivelJerarquia { get; set; }
        public string PathJerarquiaOriginal { get; set; }
        //public HierarchyId JerarquiaOriginal { get; set; }
        public short? NivelJerarquiaOriginal { get; set; }
        public string CodTipoPersona { get; set; }        
        public bool? Visibilidad { get; set; }
        public string Tipo { get; set; }
        public string FlagDescMod { get; set; }
        public ICollection<TJerarquia> Hijos { get; set; }       
        public ICollection<TJerarquiaPersona> JerarquiaPersona { get; set; }
    }
}
