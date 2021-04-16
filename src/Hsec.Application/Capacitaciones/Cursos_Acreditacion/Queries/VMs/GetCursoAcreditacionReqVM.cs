using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Cursos_Acreditacion.Queries.VMs
{
    public class GetCursoAcreditacionReqVM
    {
        public string temaCap { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string codPersona { get; set; }
        public int pagina { get; set; }
        public int paginaTamanio { get; set; }
    }
}
