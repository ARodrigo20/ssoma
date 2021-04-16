using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Command.CreateCurso.VMs
{
    public class CreateCursoPosicionVM
    {
        public string codTemaCapacita { get; set; }
        public string competencia { get; set; }
        public string curso { get; set; }
        public string codTipo { get; set; }
        public string codPosicion { get; set; }
        public bool tipo { get; set; }
        public bool estado { get; set; }
    }
}
