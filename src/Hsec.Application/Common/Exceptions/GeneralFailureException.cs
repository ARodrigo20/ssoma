using System;

namespace Hsec.Application.Common.Exceptions
{
    public class GeneralFailureException : Exception
    {
        public GeneralFailureException(string name)
         : base(name)
        {
        }
    }
}
