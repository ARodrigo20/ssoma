using System.Collections.Generic;

namespace Hsec.Application.General.Aprobaciones.Commands.UpdateProceso
{
    public class UpdateProcesoVM
    {
        public string CodProceso { get; set; }
        public string Descripcion { get; set; }
        public IList<UpdateCadenaAprobacionDto> Lista { get; set; }
    }
}