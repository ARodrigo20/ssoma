using System.Collections.Generic;

namespace Hsec.Application.General.Maestro.Queries.GetMaestroData
{
    public class MaestroDataVM
    {
        public string CodTabla { get; set; }
        public ICollection<CampoDto> Tipos { get; set; }
        public MaestroDataVM(string tabla)
        {
            this.CodTabla = tabla;
            Tipos = new HashSet<CampoDto>();
        }
    }
}