using System;

namespace Hsec.Domain.Entities.Movil
{
    public class TResponsables
    {
        public int Correlativo { get; set; }
        public string CodPersona { get; set; }
        public int? CodPosGerencia { get; set; }
        public int CodPosSuperInt { get; set; }
        public string CodTipo { get; set; }
        public DateTime? FecCreacion { get; set; }
        public string UsuCreacion { get; set; }
        public DateTime? FecModifica { get; set; }
        public string UsuModifica { get; set; }
        public string Estado { get; set; }
    }
}
