using ITCanCook_BusinessObject;
using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Implement;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace ITCanCook.Controllers
{
	[Route("api/v1/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmailService _emailService;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly IAccountService _accountService;
		public AuthController(UserManager<ApplicationUser> userManager, IEmailService emailService,
			RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration,
			IAccountService accountService)
		{
			_userManager = userManager;
			_emailService = emailService;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_configuration = configuration;
			_accountService = accountService;
		}

		[HttpPost("SignUp")]
		public async Task<IActionResult> Signup(RegisterAccountModel model)
		{
			var result = await _accountService.RegisterAccount(model);
			var statusCode = (int)result.Status;
			if (result.Status == 200)
			{
				//sendMail
				var user = await _userManager.FindByEmailAsync(model.Email);
				var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				var callbackUrl = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = user.Email }, protocol: Request.Scheme);
				_emailService.SenMail(user.Email, "Confirm Email", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
			}
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};

			return jsonResult;
		}
		
		[HttpPost("SignIn")]
		public async Task<IActionResult> SignInAsync(LoginModel model)
		{
			var result = await _accountService.LoginAccount(model);
			var statusCode = (int)result.Status;
			if(result.Status == 401)
			{
				//Resend mail
				var user = await _userManager.FindByEmailAsync(model.Email);
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				var callbackUrl = Url.Action(nameof(ConfirmEmail), "Auth", new { token = code, email = user.Email }, protocol: Request.Scheme);
				_emailService.SenMail(user.Email, "ReConfirm Email", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
			}
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};
			return jsonResult;
		}

		private async Task<IActionResult> ConfirmEmail(string token, string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				var result = await _userManager.ConfirmEmailAsync(user, token);
				if (result.Succeeded)
				{
					return StatusCode(StatusCodes.Status200OK,
						new ResponseObject { Status = 200, Message = "Email verified successfully" });
				}

			}
			return StatusCode(StatusCodes.Status500InternalServerError,
						new ResponseObject { Status = 400, Message = "This user Doesn't exist!" });
		}
		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordModel model)
		{
			var result = await _accountService.ForgotPasswordAsync(model);
			var statusCode = (int)result.Status;
			if (result.Status == 200)
			{
				// Gửi mã đặt lại mật khẩu đến email người dùng
				var code = result.Message;
				_emailService.SenMail(model.Email, "Reset password", "This is your reset code: " + code);
				result.Message = "Hãy kiểm tra mã trong mail";
			}
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};
			return jsonResult;
		}

		[HttpPost("ResetPassword")]
		public async Task<IActionResult> ResetPasswordAsync(ResetPasswordModel model)
		{
			var result = await _accountService.ResetPasswordAsync(model);
			var statusCode = (int)result.Status;
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};
			return jsonResult;
		}

	}
}
