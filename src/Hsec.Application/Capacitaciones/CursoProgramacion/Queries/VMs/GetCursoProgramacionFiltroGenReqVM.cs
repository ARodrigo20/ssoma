using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs
{
    public class GetCursoProgramacionFiltroGenReqVM
    {  
        public string codigo { get; set; }
        public string temaCap { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
        public int nroRegistros { get; set; }
        public string codPersona { get; set; }
        public int pagina { get; set; }
        public int paginaTamanio { get; set; }
    }
}