using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel
{
	public class RegisterAccountModel
	{
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; } = string.Empty;
		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; } = string.Empty;
		[Required(ErrorMessage = "Password confirm is required")]
		public string ConfirmPassword { get; set; } = string.Empty;
		[Required(ErrorMessage = "FistName is required")]
		public string Name { get; set; } = string.Empty;
		[Required(ErrorMessage = "LastName is required")]
		public DateTime Dob { get; set; }
	}
}
