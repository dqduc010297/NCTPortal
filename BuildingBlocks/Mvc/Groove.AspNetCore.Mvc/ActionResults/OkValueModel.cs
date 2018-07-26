using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.Mvc.ActionResults
{
    public class OkValueModel
    {
        public object Value { get; set; }
        public OkValueModel(object value)
        {
            this.Value = value;
        }
    }
}
