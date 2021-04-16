using MediatR;

namespace Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries.VMs
{
    public class GetCursoPosicionResponseVM
    {
        public string competencia { get; set; }
        public string codTemaCapacita { get; set; }
        public string curso { get; set; }
        public string codTipo { get; set; }
        public string codPosicion { get; set; }
        public bool bd { get; set; }
        public bool tipo { get; set; }

    }
}
