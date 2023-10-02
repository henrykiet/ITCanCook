using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITCanCook_DataAcecss.Enum;
using Microsoft.AspNetCore.Identity;
namespace ITCanCook_DataAcecss.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
		public string LastName { get; set; }
        public string? ResetPasswordCode { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime Dob { get; set; }
    }
}
