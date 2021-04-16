using System;

namespace Hsec.Application.Common.Exceptions
{
    public class TipoJerarquiaException : Exception
    {
        public TipoJerarquiaException()
          : base($"No se puede Crear .... debido a que no existe dicho tipo de jerarquia Elija 'G', 'S' ó 'O'")
        {
        }
    }
}
