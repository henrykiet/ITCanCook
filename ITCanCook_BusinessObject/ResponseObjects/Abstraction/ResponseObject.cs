using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ResponseObjects.Abstraction
{
	public class ResponseObject
	{
		public string Status { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
	}
}
