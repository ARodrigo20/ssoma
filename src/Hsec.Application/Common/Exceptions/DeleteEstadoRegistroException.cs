using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Common.Exceptions
{
    public class DeleteEstadoRegistroException : Exception
    {
        public DeleteEstadoRegistroException(string name, object key)
          : base($"No se puede Eliminar .... debido a que su estado es 'INACTIVO'  \"{name}\" ({key})")
        {
        }
    }
}
