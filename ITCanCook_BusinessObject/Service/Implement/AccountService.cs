using ITCanCook_BusinessObject.ResponseObjects;
using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITCanCook_BusinessObject.Service.Implement
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmailService _emailService;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IConfiguration _configuration;
		public AccountService(IAccountRepository accountRepository, UserManager<ApplicationUser> userManager,
			IEmailService emailService, RoleManager<IdentityRole> roleManager,
			SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
		{
			_accountRepository = accountRepository;
			_userManager = userManager;
			_emailService = emailService;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}

		public async Task<List<AccountModel>> GetAllUsersAsync()
		{
			// Sử dụng _accountRepository để lấy danh sách người dùng và chuyển đổi thành AccountModel
			var users = await _accountRepository.GetAllUsersAsync();
			return users.ConvertAll(user => new AccountModel
			{
				Email = user.Email,
				
				Dob = user.Dob
			});
		}

		public async Task<AccountModel?> GetUserByIdAsync(Guid id)
		{
			// Sử dụng _accountRepository để lấy người dùng theo ID và chuyển đổi thành AccountModel
			var user = await _accountRepository.GetUserByIdAsync(id);
			if (user == null)
			{
				return null;
			}

			return new AccountModel
			{
				Email = user.Email,
				Name = user.Name,
				Dob = user.Dob
			};
		}

		public async Task<string> CreateUserAsync(AccountModel user)
		{
			// Chuyển đổi từ AccountModel thành ApplicationUser (nếu cần)
			var applicationUser = new ApplicationUser
			{
				Email = user.Email,
				Name = user.Name,
				Dob = user.Dob,
				EmailConfirmed = true,
			};

			// Sử dụng _accountRepository để tạo người dùng
			await _accountRepository.CreateUserAsync(applicationUser);
			return applicationUser.Id;
		}

		public async Task UpdateUserAsync(AccountModel user)
		{
			// Chuyển đổi từ AccountModel thành ApplicationUser (nếu cần)
			var applicationUser = new ApplicationUser
			{
				Email = user.Email,
				Name= user.Name,
				Dob = user.Dob,
			};

			// Sử dụng _accountRepository để cập nhật người dùng
			await _accountRepository.UpdateUserAsync(applicationUser);
		}

		public async Task DeleteUserAsync(AccountModel user)
		{
			// Chuyển đổi từ AccountModel thành ApplicationUser (nếu cần)
			var applicationUser = new ApplicationUser
			{
				Email = user.Email,
				Name = user.Name,
				Dob = user.Dob
			};

			// Sử dụng _accountRepository để xóa người dùng
			await _accountRepository.DeleteUserAsync(applicationUser);
		}
		#region đăng ký 
		public async Task<ResponseObject> RegisterAccount(RegisterAccountModel model)
		{
			var result = new PostRequestResponse();
			//check confirm password
			if (!string.Equals(model.ConfirmPassword, model.Password))
			{
				result.Status = 400;
				result.Message = "Mật khẩu xác nhận không chính xác!";
				return result;
			}
			if (await _roleManager.RoleExistsAsync("User"))
			{
				// Check if user exists
				var userExist = await _userManager.FindByEmailAsync(model.Email);
				if (userExist != null)
				{
					result.Status = 400;
					result.Message = "Email đã tồn tại!";
					return result;
				}
				//create user
				var user = new ApplicationUser()
				{
					Email = model.Email,
					Name = model.Name,
					UserName = model.Email,
					Dob = model.Dob,
				};
				var resultUser = await _userManager.CreateAsync(user, model.Password);
				// Kiểm tra xem quá trình tạo người dùng có thành công không
				if (resultUser.Succeeded)
				{
					// Gán vai trò "User" cho người dùng
					await _userManager.AddToRoleAsync(user, "User");

					//Success
					result.Status = 200;
					result.Message = "Đăng ký thành công, hãy kiểm tra mail của bạn";
				}
				else
				{
					result.Status = 400;
					result.Message = "Tạo người dùng thất bại!";
				}
			}
			else
			{
				result.Status = 400;
				result.Message = "Vai trò không tồn tại!";
			}
			return result;
		}
		#endregion
		#region Đăng nhập Gmail
		public async Task<ResponseObject> LoginAccount(LoginModel model)
		{
			var result = new PostRequestResponse();
			// Kiểm tra xem email tồn tại hay không
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				result.Status = 400;
				result.Message = "Email không tồn tại!";
			}
			else if (user != null)
			{
				// Kiểm tra xác thực bằng mật khẩu
				var resultLogin = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

				if (resultLogin.Succeeded)
				{
					// Kiểm tra xem tài khoản đã xác thực email chưa
					if (!user.EmailConfirmed)
					{
						//// Gửi lại email xác thực nếu cần
						//user = await _userManager.FindByEmailAsync(model.Email);
						//var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
						//var callbackUrl = Url.Action(nameof(ConfirmEmail), "Auth", new { token = code, email = user.Email }, protocol: Request.Scheme);
						//_emailService.SenMail(user.Email, "ReConfirm Email", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

						// Trả về thông báo rằng email chưa được xác thực
						result.Status = 401;
						result.Message = "Email của bạn chưa được xác thực, hãy kiểm tra lại email!";
						return result;
					}
					// Đặt lại số lần thử đăng nhập sai thành 0
					user.FailedLoginAttempts = 0;
					await _userManager.UpdateAsync(user);

					// Lấy danh sách vai trò của người dùng
					var userRoles = await _userManager.GetRolesAsync(user);

					// Mã xác thực người dùng
					var authClaims = new List<Claim> 
						{
						new Claim(ClaimTypes.Email, model.Email),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
						};

					// Thêm claims cho vai trò của người dùng
					authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

					var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:IssuerSigningKey"]));
					var token = new JwtSecurityToken(
						issuer: _configuration["JWT:ValidIssuer"],
						audience: _configuration["JWT:ValidAudience"],
						expires: DateTime.Now.AddMinutes(30),
						claims: authClaims,
						signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
					);

					// Trả về mã JWT dưới dạng JSON
					result.Status = 200;
					result.Message = new JwtSecurityTokenHandler().WriteToken(token).ToString();
				}
				else if (resultLogin.IsLockedOut)
				{
					result.Status = 400;
					result.Message = "Tài khoản đã bị khóa! Vui lòng liên hệ quản trị viên";
					return result;
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

						result.Status = 400;
						result.Message = "Hãy thử lại sau 10 phút!";
						return result;
					}
					else
					{
						await _userManager.UpdateAsync(user); // Cập nhật số lần đăng nhập sai
						result.Status = 400;
						result.Message = "Sai mật khẩu!";
					}
				}
			}
			return result;
		}
		#endregion
		#region quên mật khẩu
		public async Task<ResponseObject> ForgotPasswordAsync(ForgotPasswordModel model)
		{
			var result = new PostRequestResponse();
			// Kiểm tra xem email tồn tại hay không
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				result.Status = 400;
				result.Message = "Email không tồn tại!";
				return result;
			}

			// Tạo mã đặt lại mật khẩu (mã ngẫu nhiên 6 chữ số)
			var code = GenerateRandomCode(6);

			// Liên kết mã với email người dùng (lưu trữ mã vào cơ sở dữ liệu hoặc tài khoản)
			user.ResetPasswordCode = code;
			await _userManager.UpdateAsync(user);

			result.Status = 200;
			result.Message = code;
			return result;
		}
		private string GenerateRandomCode(int length)
		{
			var random = new Random();
			const string chars = "0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public async Task<ResponseObject> ResetPasswordAsync(ResetPasswordModel model)
		{
			var result = new PostRequestResponse();
			// Kiểm tra email và mã đặt lại mật khẩu
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				result.Status = 400;
				result.Message = "Email không tồn tại!";
				return result;
			}

			// Kiểm tra xem mã đặt lại mật khẩu có đúng không
			if (user.ResetPasswordCode != model.Code)
			{
				result.Status = 400;
				result.Message = "Code sai!";
				return result;
			}

			// Đặt lại mật khẩu cho người dùng
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var resultReset = await _userManager.ResetPasswordAsync(user, token, model.Password);
			if (resultReset.Succeeded)
			{
				// Xóa mã đặt lại mật khẩu sau khi đã sử dụng
				user.ResetPasswordCode = string.Empty;
				await _userManager.UpdateAsync(user);

				result.Status = 200;
				result.Message = "Mật khẩu đã được reset!";
				return result;
			}
			else
			{
				result.Status = 400;
				result.Message = "Đặt lại mật khẩu thất bại!";
				return result;
			}
		}

		#endregion

		public async Task<bool> UpdateProfile(UpdateProfile model)
		{
			var result = false;
			var account = await _accountRepository.GetUserByIdAsync(model.Id);
			if (account != null)
			{
				// Cập nhật thông tin tài khoản
				account.Id = model.Id.ToString();
				account.Dob = model.Dob;
				account.Name = model.FirstName;

				// Sử dụng _accountRepository để cập nhật người dùng
				await _accountRepository.UpdateUserAsync(account);

				result = true;
			}
			return result;
		}

	}
}
