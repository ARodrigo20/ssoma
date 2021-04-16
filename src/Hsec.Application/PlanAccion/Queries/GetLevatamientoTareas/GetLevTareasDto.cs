using System;
using System.Collections.Generic;

namespace Hsec.Application.PlanAccion.Queries.GetLevatamientoTareas
{
    public class GetLevTareasDto
    {
        public GetLevTareasDto()
        {
            files = new List<ModelFileVM>();
        }
        public int codAccion { get; set; }
        public string codPersona { get; set; }
        public int correlativo { get; set; }
        public string descripcion { get; set; }
        public DateTime? fecha { get; set; }
        public double porcentajeAvance { get; set; }
        public bool estado { get; set; }
        public string nombres { get; set; }
        public IList<ModelFileVM> files { get; set; }

    }
}
