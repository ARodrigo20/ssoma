using System.Collections.Generic;

namespace Hsec.Application.Verificaciones.Queries.GetBuscarVerificacion
{
    public class BuscarVerificacionVM
    {
        public HashSet<VerificacionBuscarDto> Lists { get;set;}
        public int Count { get; set; }
        public BuscarVerificacionVM()
        {
        }
    }
}