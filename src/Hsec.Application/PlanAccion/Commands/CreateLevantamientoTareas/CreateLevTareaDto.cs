using System;

namespace Hsec.Application.PlanAccion.Commands.CreateLevantamientoTareas
{
    public class CreateLevTareaDto
    {
        public string nroDocReferencia { get; set; }
        public string nroSubDocReferencia { get; set; }
        public int codAccion { get; set; }
        public string codPersona { get; set; }
        public int correlativo { get; set; }
        public string descripcion { get; set; }
        public DateTime? fecha { get; set; }
        public double porcentajeAvance { get; set; }
        public bool estado { get; set; }


    }
}
