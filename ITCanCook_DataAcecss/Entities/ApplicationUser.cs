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
        public string Name { get; set; }
		public Gender Gender { get; set; }
        public float Hight { get; set; }
        public float Weight { get; set; }
        public DateTime Dob { get; set; }
        public string? ResetPasswordCode { get; set; }
        public int FailedLoginAttempts { get; set; }

	}
}
