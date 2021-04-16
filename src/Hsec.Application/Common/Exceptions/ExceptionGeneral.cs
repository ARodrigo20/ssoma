using System;

namespace Hsec.Application.Common.Exceptions
{
    public class ExceptionGeneral : Exception
    {
        public ExceptionGeneral(string name)
        : base(name)
        {
        }
    }
}
