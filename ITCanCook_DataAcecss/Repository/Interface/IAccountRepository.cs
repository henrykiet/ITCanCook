using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository.Interface
{
	public interface IAccountRepository
	{
		Task<List<ApplicationUser>> GetAllUsersAsync();
		Task<ApplicationUser?> GetUserByIdAsync(Guid id);
		Task CreateUserAsync(ApplicationUser user);
		Task UpdateUserAsync(ApplicationUser user);
		Task DeleteUserAsync(ApplicationUser user);
		Task UpdateUserPremiumStatus(string userId);
	}
}
