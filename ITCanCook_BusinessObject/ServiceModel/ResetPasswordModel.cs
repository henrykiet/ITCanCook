using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel
{
	public class ResetPasswordModel
	{
		[Required]
		public string Email { get; set; } = string.Empty;
        [Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public string Code { get; set; } = string.Empty;

	}
}
