using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Core
{
    public class DataSourceValue<TValue>
    {
        public TValue Value { get; set; }
        public string DisplayName {get;set;}
    }
}
