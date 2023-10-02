using ITCanCook_BusinessObject.ServiceModel;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Models;
using ITCanCook_DataAcecss.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ITCanCook.Controllers
{
	[Route("api/v1/accounts")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class AccountController : ControllerBase
	{
		private readonly IAccountRepository _accountRepository;

		public AccountController(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllUsersAsync()
		{
			var users = await _accountRepository.GetAllUsersAsync();
			return Ok(users);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserByIdAsync(Guid id)
		{
			var user = await _accountRepository.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			return Ok(user);
		}

		[HttpPost("CreateUser")]
		public async Task<IActionResult> CreateUserAsync(ApplicationUser user)
		{
			await _accountRepository.CreateUserAsync(user);
			return CreatedAtAction(nameof(GetUserByIdAsync), new { id = user.Id }, user);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUserAsync(Guid id, AccountModel user)
		{
			var existingUser = await _accountRepository.GetUserByIdAsync(id);
			if (existingUser == null)
			{
				return NotFound();
			}

			// Cập nhật thông tin người dùng từ dữ liệu người dùng mới
			existingUser.FirstName = user.FirstName;
			existingUser.LastName = user.LastName;
			existingUser.Email = user.Email;
			existingUser.Dob = user.Dob;

			await _accountRepository.UpdateUserAsync(existingUser);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUserAsync(Guid id)
		{
			var user = await _accountRepository.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			await _accountRepository.DeleteUserAsync(user);
			return NoContent();
		}

	}
}
