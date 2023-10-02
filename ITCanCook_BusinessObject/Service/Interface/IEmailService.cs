using ITCanCook_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
	public interface IEmailService
	{
		public bool SenMail(string to, string subject, string body);
	}
}
