using Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado.VMs
{
    public class GetFiltradoRequestVM
    {
        public GetFiltradoRequestVM()
        {
            temaCapEspecifico = new List<GetFiltradoRequestDto>();
        }

        public string codTemaCapacita { get; set; } //identity
        public string codTipoTema { get; set; }
        public string codAreaCapacita { get; set; }
        public string descripcion { get; set; }
        public string competenciaHs { get; set; }
        public string codHha { get; set; }
        public bool estado { get; set; }
        public IList<GetFiltradoRequestDto> temaCapEspecifico { get; set; }
    }
}
