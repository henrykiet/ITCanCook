using AutoMapper;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository.Implement
{
	public class AccountRepository : IAccountRepository
	{
		private readonly ITCanCookContext _context;

		public AccountRepository(ITCanCookContext context)
		{
			_context = context;
		}

		public async Task<List<ApplicationUser>> GetAllUsersAsync()
		{
			return await _context.ApplicationUsers.ToListAsync();
		}

		public async Task<ApplicationUser?> GetUserByIdAsync(Guid id)
		{
			return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id.ToString());
		}

		public async Task CreateUserAsync(ApplicationUser user)
		{
			await _context.ApplicationUsers.AddAsync(user);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateUserAsync(ApplicationUser user)
		{
			_context.ApplicationUsers.Update(user);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteUserAsync(ApplicationUser user)
		{
			_context.ApplicationUsers.Remove(user);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateUserPremiumStatus(string userId)
		{
			// Tìm người dùng theo userId
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			if (user != null)
			{
				// Cập nhật trạng thái "IsPremium" thành true (hoặc false, tùy vào logic của bạn)
				user.IsPrenium = true; // hoặc false
				await _context.SaveChangesAsync();
			}
		}
	}
}
