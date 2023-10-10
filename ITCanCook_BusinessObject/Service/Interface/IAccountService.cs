using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
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
		Task<AccountModel?> GetUserByEmailAsync(string email);
		Task<string> CreateUserAsync(CreateUserModel user);
		Task<ResponseObject> UpdateUserAsync(UpdateAccountModel user, string id);
		Task<ResponseObject> DeleteUserAsync(string userId);
		Task<ResponseObject> UpdateUserRoleAsync(string userId, List<string> roles);
		Task<ResponseObject> RegisterAccount(RegisterAccountModel account);
		Task<ResponseObject> RegisterAccount(RegisterWithPhoneModel model);
		Task<ResponseObject> LoginAccount(LoginModel model);
		Task<ResponseObject> ForgotPasswordAsync(ForgotPasswordModel model);
		Task<ResponseObject> ResetPasswordAsync(ResetPasswordModel model);
		Task<ResponseObject> UpdateProfile(UpdateProfile model);
		Task<ResponseObject> UpdatePassword(UpdatePasswordModel model);
		Task<ResponseObject> Logout();

	}
}
