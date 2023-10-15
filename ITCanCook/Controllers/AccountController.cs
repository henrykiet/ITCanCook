using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ITCanCook.Controllers
{
    [Route("api/v1/accounts")]
	[ApiController]
	
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}
		#region User
		[HttpPut("UpdateProfile")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfile accountModel)
		{
			var result = await _accountService.UpdateProfile(accountModel);
			var statusCode = (int)result.Status;
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};
			return jsonResult;
		}

		[HttpPost("UpdatePassword")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> UpdatePassword(UpdatePasswordModel model)
		{
			var result = await _accountService.UpdatePassword(model);
			var statusCode = (int)result.Status;
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};
			return jsonResult;
		}
		#endregion
		#region Admin
		[HttpGet("GetAll")]
		[Authorize(Roles = "Admin")]
		public async Task<List<AccountModel>> GetAllAccountAsync()
		{
			var result = await _accountService.GetAllUsersAsync();
			return result;
		}

		[HttpGet("GetUserById")]
		[Authorize]
		public async Task<IActionResult> GetUserById(Guid id)
		{
			var result = await _accountService.GetUserByIdAsync(id);
			if (result != null)
			{
				return new JsonResult(result)
				{
					StatusCode = 200,
				};
			}
			return new JsonResult(new
			{
				Message = "Người dùng không tồn tại",
				Status = 404
			})
			{
				StatusCode = 404,
			};
		}
		[HttpGet("GetUserByEmail")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUserByEmail(string email)
		{
			var result = await _accountService.GetUserByEmailAsync(email);
			if (result != null)
			{
				return new JsonResult(result)
				{
					StatusCode = 200,
				};
			}
			return new JsonResult(new
			{
				Message = "Người dùng không tồn tại",
				Status = 404
			})
			{
				StatusCode = 404,
			};
		}
		[HttpPost("CreateUser")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserModel user)
		{
			var userId = await _accountService.CreateUserAsync(user);
			if (!string.IsNullOrEmpty(userId))
			{
				if (userId.Equals("Email đã tồn tại!"))
				{
					return new JsonResult(new
					{
						Message = userId,
						Status = 400
					})
					{
						StatusCode = 400,
					};
				}
				return new JsonResult(new
				{
					UserId = userId,
					Message = "Tạo người dùng thành công",
					Status = 200
				})
				{
					StatusCode = 200,
				};
			}
			return new JsonResult(new
			{
				Message = userId,
				Status = 400
			})
			{
				StatusCode = 400,
			};
		}

		[HttpPut("UpdateUser")]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> UpdateUser([FromBody] UpdateAccountModel user, string id)
		{

			var result =  await _accountService.UpdateUserAsync(user, id);
			var statusCode = (int)result.Status;
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};
			return jsonResult;
		}

		[HttpDelete("DeleteUser")]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> DeleteUser(string id)
		{
			var result = await _accountService.DeleteUserAsync(id);
			var statusCode = (int)result.Status;
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};
			return jsonResult;
		}
		[HttpPut("UpdateUserRole")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateUserRole(string userId, [FromBody] List<string> roles)
		{
			var result = await _accountService.UpdateUserRoleAsync(userId, roles);
			var statusCode = (int)result.Status;
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};
			return jsonResult;
		}
		#endregion

	}
}
