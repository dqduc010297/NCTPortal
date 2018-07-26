using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Helpers
{
	public class Pagination
	{
		public static PagedData<T> PagedResult<T>(IEnumerable<T> list, int PageNumber, int Totalpage, int PageSize) where T : class
		{
			var result = new PagedData<T>();

			result.Data = list;
			result.TotalPages = Convert.ToInt32(Math.Ceiling((double)Totalpage / PageSize));
			result.CurrentPage = PageNumber;

			return result;
		}
	}
}
