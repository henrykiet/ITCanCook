using ITCanCook_BusinessObject;
using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Implement;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Models;
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
		public AuthController(UserManager<ApplicationUser> userManager, IEmailService emailService,
			RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_emailService = emailService;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}

		[HttpPost("SignUp")]
		public async Task<IActionResult> Signup(RegisterAccountModel model, string role)
		{
			//check confirm password
			if (!string.Equals(model.ConfirmPassword, model.Password))
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
						new ResponseObject { Status = "Error", Message = "ConfirmPassword doesn't match!" });
			}
			if (await _roleManager.RoleExistsAsync(role))
			{
				// Check if user exists
				var userExist = await _userManager.FindByEmailAsync(model.Email);
				if (userExist != null)
				{
					return StatusCode(StatusCodes.Status500InternalServerError,
						new ResponseObject { Status = "Error", Message = "User have exist!" });
				}
				//create user
				var user = new ApplicationUser()
				{
					Email = model.Email,
					FirstName = model.FistName,
					LastName = model.LastName,
					UserName = model.Email,
				};
				var result = await _userManager.CreateAsync(user, model.Password);
				// Kiểm tra xem quá trình tạo người dùng có thành công không
				if (result.Succeeded)
				{
					// Gán vai trò "User" cho người dùng
					await _userManager.AddToRoleAsync(user, role);
					//verifyEmail
					user = await _userManager.FindByEmailAsync(model.Email);
					var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var callbackUrl = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = user.Email }, protocol: Request.Scheme);
					_emailService.SenMail(user.Email, "Confirm Email", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
					return StatusCode(StatusCodes.Status200OK,
						new ResponseObject { Status = "Success", Message = "Signup success. Please check your email" });
				}
				else
				{
					return StatusCode(StatusCodes.Status500InternalServerError,
						new ResponseObject { Status = "Error", Message = "Create user doesn't exist!" });
				}
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
						new ResponseObject { Status = "Error", Message = "This role Doesn't exist!" });
			}
		}

		[HttpPost("SignIn")]
		public async Task<IActionResult> SignInAsync(LoginModel model)
		{
			// Kiểm tra xem email tồn tại hay không
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseObject { Status = "Error", Message = "Email Doesn't exist!" });
			}

			// Kiểm tra xác thực bằng mật khẩu
			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

			if (result.Succeeded)
			{
				// Kiểm tra xem tài khoản đã xác thực email chưa
				if (!user.EmailConfirmed)
				{
					// Gửi lại email xác thực nếu cần
					user = await _userManager.FindByEmailAsync(model.Email);
					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var callbackUrl = Url.Action(nameof(ConfirmEmail), "Auth", new { token = code, email = user.Email }, protocol: Request.Scheme);
					_emailService.SenMail(user.Email, "ReConfirm Email", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
					// Trả về thông báo rằng email chưa được xác thực
					return StatusCode(StatusCodes.Status500InternalServerError,
						new ResponseObject { Status = "Error", Message = "Email is not confirmed! Resend confirmation email." });
				}

				// Mã xác thực người dùng
				var authClaims = new List<Claim>
					{
					new Claim(ClaimTypes.Email, model.Email),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
					};

				var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:IssuerSigningKey"]));
				var token = new JwtSecurityToken(
					issuer: _configuration["JWT:ValidIssuer"],
					audience: _configuration["JWT:ValidAudience"],
					expires: DateTime.Now.AddMinutes(30),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
				);

				// Đặt lại số lần thử đăng nhập sai thành 0
				user.FailedLoginAttempts = 0;
				await _userManager.UpdateAsync(user);
				// Trả về mã JWT dưới dạng JSON
				return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
			}
			else if (result.IsLockedOut)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseObject { Status = "Error", Message = "This Account is locked!" });
			}
			else
			{
				// Đăng nhập thất bại, tăng số lần đăng nhập sai
				user.FailedLoginAttempts++;

				// Kiểm tra xem người dùng đã đăng nhập sai quá 5 lần chưa
				if (user.FailedLoginAttempts >= 5)
				{
					// Khóa tài khoản
					user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(10); // khóa account 10 phút
					await _userManager.SetLockoutEndDateAsync(user, user.LockoutEnd);

					return StatusCode(StatusCodes.Status500InternalServerError,
						new ResponseObject { Status = "Error", Message = "This Account is locked!" });
				}
				else
				{
					await _userManager.UpdateAsync(user); // Cập nhật số lần đăng nhập sai

					return StatusCode(StatusCodes.Status500InternalServerError,
						new ResponseObject { Status = "Error", Message = "Invalid login attempt!" });
				}
			}
		}
		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordModel model)
		{
			// Kiểm tra xem email tồn tại hay không
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseObject { Status = "Error", Message = "Email Doesn't exist!" });
			}

			// Tạo mã đặt lại mật khẩu (mã ngẫu nhiên 6 chữ số)
			var code = GenerateRandomCode(6);

			// Liên kết mã với email người dùng (lưu trữ mã vào cơ sở dữ liệu hoặc tài khoản)
			user.ResetPasswordCode = code;
			await _userManager.UpdateAsync(user);

			// Gửi mã đặt lại mật khẩu đến email người dùng
			_emailService.SenMail(user.Email, "Reset password", "This is your reset code: " + code);

			return StatusCode(StatusCodes.Status200OK,
				new ResponseObject { Status = "Success", Message = "Password reset instructions have been sent to your email." });
		}

		[HttpPost("ResetPassword")]
		public async Task<IActionResult> ResetPasswordAsync(ResetPasswordModel model)
		{
			// Kiểm tra email và mã đặt lại mật khẩu
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseObject { Status = "Error", Message = "Email Doesn't exist!" });
			}

			// Kiểm tra xem mã đặt lại mật khẩu có đúng không
			if (user.ResetPasswordCode != model.Code)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseObject { Status = "Error", Message = "Invalid reset code!" });
			}

			// Đặt lại mật khẩu cho người dùng
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
			if (result.Succeeded)
			{
				// Xóa mã đặt lại mật khẩu sau khi đã sử dụng
				user.ResetPasswordCode = string.Empty;
				await _userManager.UpdateAsync(user);

				return StatusCode(StatusCodes.Status200OK,
					new ResponseObject { Status = "Success", Message = "Password has been reset successfully." });
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseObject { Status = "Error", Message = "Failed to reset password." });
			}
		}


		// Hàm tạo mã ngẫu nhiên
		private string GenerateRandomCode(int length)
		{
			var random = new Random();
			const string chars = "0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}



		[HttpGet("ConfirmEmail")]
		public async Task<IActionResult> ConfirmEmail(string token, string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				var result = await _userManager.ConfirmEmailAsync(user, token);
				if (result.Succeeded)
				{
					return StatusCode(StatusCodes.Status200OK,
						new ResponseObject { Status = "Success", Message = "Email verified successfully" });
				}

			}
			return StatusCode(StatusCodes.Status500InternalServerError,
						new ResponseObject { Status = "Error", Message = "This user Doesn't exist!" });
		}


	}
}
