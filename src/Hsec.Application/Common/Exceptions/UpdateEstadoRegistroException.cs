using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Common.Exceptions
{
    public class UpdateEstadoRegistroException : Exception
    {
        public UpdateEstadoRegistroException(string name, object key)
          : base($"No se puede Modificar ... debido a que su estado es 'INACTIVO' '{name}' ({key})")
        {
        }
    }
}
