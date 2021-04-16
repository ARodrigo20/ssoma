using System;

namespace Hsec.Domain.Entities.Movil
{
    public class TFeedback
    {
        public int Correlativo { get; set; }
        public string CodUsuario { get; set; }
        public DateTime? Fecha { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
    }
}
