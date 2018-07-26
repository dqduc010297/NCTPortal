using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.Common.Identity
{
    public class UserIdentity<TType>
    {
        public TType Id { get; set; }
        public string UserName { get; set; }

    }
}
