using System.Collections.Generic;

namespace Hsec.Application.General.Aprobaciones.Queries.GetDocAprobacion
{
    public class GetDocAprobacionVM
    {
        public List<DocAprobacionDto> list { get; set; }
        public GetDocAprobacionVM() {
            list = new List<DocAprobacionDto>();
        }
    }

    public class DocAprobacionDto
    {
        public string DocReferencia { get; set; }
        public string CodTabla { get; set; }
    }
}