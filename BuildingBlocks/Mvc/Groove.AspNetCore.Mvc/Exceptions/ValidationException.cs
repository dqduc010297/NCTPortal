using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.Mvc.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            :base(message)
        {
        }
        
    }
}
