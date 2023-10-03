using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel;
using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ITCanCook.Controllers
{
	[Route("api/v1/accounts")]
	[ApiController]
	//[Authorize]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPut("UpdateProfile")]
		//[Authorize(Roles = "CUSTOMER")]
		public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfile accountModel)
		{
			var result = await _accountService.UpdateProfile(accountModel);
			if (result)
			{
				return new JsonResult(new
				{
					Message = "Cập nhật thông tin tài khoản thành công",
					Status = 200
				})
				{
					StatusCode = 200,
				};
			}
			return new JsonResult(new
			{
				Message = "Cập nhật thông tin tài khoản thất bại",
				Status = 400
			})
			{
				StatusCode = 400,
			};
		}

		//[HttpPut]
		//[Route("password")]
		////[Authorize(Roles = "CUSTOMER")]
		//public IActionResult UpdatePassword([FromBody] UpdatePasswordModel updateModel)
		//{
		//	_accountService.Update(updateModel);
		//	if (_unitOfWork.Commit())
		//	{
		//		return new JsonResult(new
		//		{
		//			Message = "Cập nhật mật khẩu thành công",
		//			Status = 201
		//		})
		//		{
		//			StatusCode = 201,
		//		};
		//	}
		//	return new JsonResult(new
		//	{
		//		Message = "Cập nhật mật khẩu thất bại",
		//		Status = 400
		//	})
		//	{
		//		StatusCode = 400,
		//	};
		//}
	}
}
