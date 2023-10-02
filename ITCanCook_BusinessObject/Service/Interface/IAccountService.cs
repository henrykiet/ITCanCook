using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
	public interface IAccountService
	{
		public Task<IdentityResult> SignUpAsync(RegisterAccountModel registerAccountModel, string role);
		public Task<string> SignInAsync(LoginModel loginModel);
		public Task<IdentityResult> ConfirmEmail(string token, string email);
	}
}
