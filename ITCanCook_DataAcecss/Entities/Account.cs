using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Entities
{
	[Table("User")]
	public class Account
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		public string FullName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email is not valid")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }

		public DateTime Dob { get; set; }

		public Role Role { get; set; }

		public AccountStatus Status { get; set; }
		public List<RecipeAmount> RecipeAmounts { get; set; }

	}
}
