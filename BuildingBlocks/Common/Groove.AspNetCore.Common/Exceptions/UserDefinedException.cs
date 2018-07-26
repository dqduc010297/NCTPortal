using Groove.AspNetCore.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.Common.Exceptions
{
    public class UserDefinedException : Exception
    {
        public ExceptionMessage UserDefinedMessage;
        public UserDefinedException() { }
        public UserDefinedException(ExceptionMessage message)
            : base(message.Message)
        {
            UserDefinedMessage = message;
        }
        public UserDefinedException(string message) : base(message)
        {
            UserDefinedMessage = new ExceptionMessage(message);
        }
    }
}
