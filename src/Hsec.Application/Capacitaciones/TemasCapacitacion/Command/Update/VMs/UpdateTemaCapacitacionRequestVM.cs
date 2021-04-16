using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Update.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Update.VMs
{
    public class UpdateTemaCapacitacionRequestVM
    {
        public UpdateTemaCapacitacionRequestVM() {
            temaCapEspecifico = new List<UpdateTemaCapacitacionRequestDto>();
        }
        public string codTemaCapacita { get; set; }
        public string codTipoTema { get; set; } // todo lo q esta en la tabla maestro es STRING
        public string codAreaCapacita { get; set; }
        public string descripcion { get; set; }
        public string competenciaHs { get; set; }
        public string codHha { get; set; }
        public IList<UpdateTemaCapacitacionRequestDto> temaCapEspecifico { get; set; }



    }
}
