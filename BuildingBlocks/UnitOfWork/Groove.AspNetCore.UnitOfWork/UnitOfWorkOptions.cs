using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.UnitOfWork
{
    public class UnitOfWorkOptions
    {
        public TimeSpan? Timeout { get; set; }
    }
}
