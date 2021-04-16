using System.Collections.Generic;

namespace Hsec.Application.General.Modulos.Queries.GetModulo
{
    public class ModuloNodeVM
    {
        public ModuloNodeVM()
        {
            this.children = new HashSet<ModuloNodeVM>();
        }
        public string label { get; set; }
        public string data { get; set; }
        public ICollection<ModuloNodeVM> children { get; set; }
    }
}
