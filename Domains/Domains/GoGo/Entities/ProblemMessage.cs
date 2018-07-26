using Groove.AspNetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Entities
{
    public class ProblemMessage : IEntity<int>
    {
		public int Id { get; set; }

		public int RequestId { get; set; }
		public Request Request { get; set; }
        public bool IsSolve { set; get; }
		public string Message { get; set; }
    }
}
