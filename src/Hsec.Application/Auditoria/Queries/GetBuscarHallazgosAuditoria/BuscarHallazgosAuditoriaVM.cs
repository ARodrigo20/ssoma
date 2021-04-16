using System.Collections.Generic;

namespace Hsec.Application.Auditoria.Queries.GetBuscarHallazgosAuditoria
{
    public class BuscarHallazgosAuditoriaVM
    {
        public BuscarHallazgosAuditoriaVM()
        {
            this.list = new HashSet<HallazgoDto>();
        }

        public int Count { get; set; }
        public int Pagina { get; set; }
        public ICollection<HallazgoDto> list { get; set; }
    }
}