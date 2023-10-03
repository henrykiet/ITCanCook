using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.ServiceModel;
using ITCanCook_DataAcecss.Entities;
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
		Task<List<AccountModel>> GetAllUsersAsync();
		Task<AccountModel?> GetUserByIdAsync(Guid id);
		Task<string> CreateUserAsync(AccountModel user);
		Task UpdateUserAsync(AccountModel user);
		Task DeleteUserAsync(AccountModel user);
		Task<ResponseObject> RegisterAccount(RegisterAccountModel account);
		Task<ResponseObject> LoginAccount(LoginModel model);
		Task<ResponseObject> ForgotPasswordAsync(ForgotPasswordModel model);
		Task<ResponseObject> ResetPasswordAsync(ResetPasswordModel model);
		Task<bool> UpdateProfile(UpdateProfile model);


	}
}
