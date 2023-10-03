using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel
{
	public class TokenModel
	{
		public string Token { get; set; } = string.Empty;
		public DateTime Expiration { get; set; }
		public string RefreshToken { get; set; } = string.Empty;
	}
}
