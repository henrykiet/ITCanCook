using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ResponseObjects.Abstraction
{
	public class ResponseObject
	{
		public int Status { get; set; }
		public string Message { get; set; } = string.Empty;
	}
}
