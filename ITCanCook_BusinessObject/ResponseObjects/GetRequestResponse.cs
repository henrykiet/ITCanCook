using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ResponseObjects
{
	public class GetRequestResponse<T> : ResponseObject
	{
		public T Data { get; set; }
	}
}
