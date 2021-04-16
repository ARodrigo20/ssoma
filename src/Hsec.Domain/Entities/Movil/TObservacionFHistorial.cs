using System;

namespace Hsec.Domain.Entities.Movil
{
    public class TObservacionFHistorial
    {
        public int Correlativo { get; set; }
        public string CodObsFacilito { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Comentario { get; set; }
        public DateTime? FecCreacion { get; set; }
        public string UsuCreacion { get; set; }
        public DateTime? FecModifica { get; set; }
        public string UsuModifica { get; set; }
        public string Estado { get; set; }
    }
}
