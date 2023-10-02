using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Models
{
	public class RegisterAccountModel
	{
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Password confirm is required")]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "FistName is required")]
		public string FistName { get; set; }
		[Required(ErrorMessage = "LastName is required")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Birthday is required")]

		public DateTime Dob { get; set; }
	}
}
