using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.General.Accesos.Queries.GetAccesos
{
    public class AccesoVM
    {
        public AccesoVM()
        {
            this.Hijos = new HashSet<AccesoVM>();
        }
        public int CodAcceso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; } // texto alternativo al nombre- tooltip
        public string Componente { get; set; }
        public string Icono { get; set; }
        public string BadgeVariant { get; set; }
        public string BadgeText { get; set; }       
        public ICollection<AccesoVM> Hijos { get; set; }
    }
}
