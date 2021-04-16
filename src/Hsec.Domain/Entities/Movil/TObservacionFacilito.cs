using System;

namespace Hsec.Domain.Entities.Movil
{
    public class TObservacionFacilito
    {
        public string CodObsFacilito { get; set; }
        public string Tipo { get; set; }
        public string CodPosicionGer { get; set; }
        public string CodPosicionSup { get; set; }
        public string UbicacionExacta { get; set; }
        public string Observacion { get; set; }
        public string Accion { get; set; }
        public DateTime? FechaFin { get; set; }
        public string RespAuxiliar { get; set; }
        public DateTime? FecCreacion { get; set; }
        public string UsuCreacion { get; set; }
        public DateTime? FecModifica { get; set; }
        public string UsuModifica { get; set; }
        public string Estado { get; set; }
    }
}
