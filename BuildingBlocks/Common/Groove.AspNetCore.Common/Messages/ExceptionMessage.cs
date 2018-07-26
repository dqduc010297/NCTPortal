using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Groove.AspNetCore.Common.Messages
{
    // This structure follow Microsoft/api-guidelines
    public class ExceptionMessage
    {
        public string Message { get; set; }
        public List<ExceptionMessage> Details { get; set; }
        public ExceptionMessage()
        {
        }
        public ExceptionMessage(string message)
        {
            this.Message = message;
        }
        
    }
}
