using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Create.DTOs;
using Hsec.Domain.Entities.Capacitaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Create.VMs
{
    public class CreateTemaCapacitacionRequestVM /*: IMapFrom<TTemaCapEspecifico>*/
    {
        public CreateTemaCapacitacionRequestVM() {     
            temaCapEspecifico = new List<CreateTemaCapacitacionRequestCapEspDto>();
        }

        public string codTipoTema { get; set; } // todo lo q esta en la tabla maestro es STRING
        public string codAreaCapacita { get; set; }
        public string descripcion { get; set; }
        public string competenciaHs { get; set; }
        public string codHha { get; set; }
        public IList<CreateTemaCapacitacionRequestCapEspDto> temaCapEspecifico { get; set; }
    }
}
