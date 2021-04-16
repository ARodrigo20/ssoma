using System.Collections.Generic;

namespace Hsec.Application.General.Modulos.Queries.GetModulo
{
    public class ModuloVM
    {
        public ModuloVM()
        {
            this.data = new HashSet<ModuloNodeVM>();
        }
        public ICollection<ModuloNodeVM> data { get; set; }
        public int count { get; set; }
    }
}
