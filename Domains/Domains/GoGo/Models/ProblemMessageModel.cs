using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models
{
    public class ProblemMessageModel
    {
        public int ProblemID { set; get; }
        public string RequestCode { set; get; }
        public string Message { set; get; }
        public bool IsSolve { set; get; }
    }
}
