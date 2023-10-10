using ITCanCook_BusinessObject.ResponseObjects;
using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Interface;
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
using System.Data;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;

namespace ITCanCook_BusinessObject.Service.Implement
{
    public class AccountService : IAccountService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IConfiguration _configuration;
		public AccountService(IAccountRepository accountRepository, UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
		{
			_accountRepository = accountRepository;
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}
		#region Quản lý User
		public async Task<List<AccountModel>> GetAllUsersAsync()
		{
			// Sử dụng _accountRepository để lấy danh sách người dùng và chuyển đổi thành AccountModel
			var users = await _accountRepository.GetAllUsersAsync();
			return users.ConvertAll(user => new AccountModel
			{
				Id = user.Id,
				Email = user.Email,
				Name = user.Name,
				Gender = user.Gender,
				Hight = user.Hight,
				Weight = user.Weight,
				Dob = user.Dob,
				Phone = user.PhoneNumber,
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
				Id = user.Id,
				Email = user.Email,
				Name = user.Name,
				Dob = user.Dob,
				Gender = user.Gender,
				Hight = user.Hight,
				Phone = user.PhoneNumber,
				Weight = user.Weight,

			};
		}
		public async Task<AccountModel?> GetUserByEmailAsync(string email)
		{
			// Sử dụng UserManager để tìm người dùng theo email
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return null;
			}

			// Chuyển đổi thành AccountModel và trả về
			return new AccountModel
			{
				Id = user.Id,
				Email = user.Email,
				Name = user.Name,
				Dob = user.Dob,
				Gender = user.Gender,
				Hight = user.Hight,
				Phone = user.PhoneNumber,
				Weight = user.Weight
			};
		}
	
			public async Task<string> CreateUserAsync(CreateUserModel user)
		{
			// Kiểm tra xem email đã tồn tại hay chưa
			var existingUser = await _userManager.FindByEmailAsync(user.Email);
			if (existingUser != null)
			{
				// Email đã tồn tại, bạn có thể xử lý lỗi ở đây nếu cần
				return "Email đã tồn tại!";
			}
			// Chuyển đổi từ CreateUserModel thành ApplicationUser (nếu cần)
			var applicationUser = new ApplicationUser
			{
				Email = user.Email,
				Name = user.Name,
				EmailConfirmed = false,
				UserName = user.Email,
			};

			var resultCreate = await _userManager.CreateAsync(applicationUser);

			if (resultCreate.Succeeded)
			{

				// Đặt mật khẩu mặc định cho người dùng
				var defaultPassword = "Abc123@"; //(hoặc bất kỳ mật khẩu nào bạn muốn)
				await _userManager.AddPasswordAsync(applicationUser, defaultPassword);

				// Gán vai trò cho người dùng
				await _userManager.AddToRoleAsync(applicationUser, "User");

				// Tìm người dùng theo email
				var createdUser = await _userManager.FindByEmailAsync(user.Email);

				if (createdUser != null)
				{
					// Lấy ID của người dùng và trả về
					var userId = createdUser.Id;
					return userId;
				}
			}

			// Xử lý các lỗi tạo người dùng nếu cần
			return "";
		}


		public async Task<ResponseObject> UpdateUserAsync(UpdateAccountModel user, string id)
		{
			var result = new ResponseObject();

			try
			{
				var existingUser = await _userManager.FindByIdAsync(id);

				if (existingUser == null)
				{
					result.Status = 404; // Không tìm thấy người dùng
					result.Message = "Người dùng không tồn tại!";
				}
				else
				{
					// Cập nhật thông tin của người dùng từ model
					existingUser.Email = user.Email;
					existingUser.Name = user.Name;
					existingUser.Dob = user.Dob;
					existingUser.Hight = user.Hight;
					existingUser.Gender = user.Gender;
					existingUser.Weight = user.Weight;
					existingUser.PhoneNumber = user.Phone;

					var updateResult = await _userManager.UpdateAsync(existingUser);

					if (updateResult.Succeeded)
					{
						result.Status = 200;
						result.Message = "Cập nhật người dùng thành công";
					}
					else
					{
						result.Status = 400; // Lỗi yêu cầu không hợp lệ
						result.Message = "Cập nhật người dùng thất bại!";
					}
				}
			}
			catch (Exception ex)
			{
				result.Status = 500; // Lỗi server
				result.Message = $"Lỗi trong quá trình cập nhật người dùng: {ex.Message}";
			}

			return result;
		}


		public async Task<ResponseObject> DeleteUserAsync(string userId)
		{
			var result = new ResponseObject();

			try
			{
				var user = await _userManager.FindByIdAsync(userId);

				if (user == null)
				{
					result.Status = 404; // Không tìm thấy người dùng
					result.Message = "Người dùng không tồn tại!";
				}
				else
				{
					var deleteResult = await _userManager.DeleteAsync(user);

					if (deleteResult.Succeeded)
					{
						result.Status = 200;
						result.Message = "Xóa người dùng thành công";
					}
					else
					{
						result.Status = 400; // Lỗi yêu cầu không hợp lệ
						result.Message = "Xóa người dùng thất bại!";
					}
				}
			}
			catch (Exception ex)
			{
				result.Status = 500; // Lỗi server
				result.Message = $"Lỗi trong quá trình xóa người dùng: {ex.Message}";
			}

			return result;
		}
		
		public async Task<ResponseObject> UpdateUserRoleAsync(string userId, List<string> roles)
		{
			var result = new ResponseObject();

			try
			{
				// Tìm người dùng bằng ID
				var user = await _userManager.FindByIdAsync(userId);

				if (user == null)
				{
					result.Status = 404; // Không tìm thấy người dùng
					result.Message = "Người dùng không tồn tại!";
				}
				else
				{
					// Lấy danh sách vai trò hiện tại của người dùng
					var userRoles = await _userManager.GetRolesAsync(user);

					// Loại bỏ người dùng khỏi các vai trò hiện tại (nếu có)
					await _userManager.RemoveFromRolesAsync(user, userRoles);

					// Thêm người dùng vào các vai trò mới
					var addResult = await _userManager.AddToRolesAsync(user, roles);

					if (addResult.Succeeded)
					{
						result.Status = 200;
						result.Message = "Cập nhật vai trò người dùng thành công";
					}
					else
					{
						result.Status = 400; // Lỗi yêu cầu không hợp lệ
						result.Message = "Cập nhật vai trò người dùng thất bại!";
					}
				}
			}
			catch (Exception ex)
			{
				result.Status = 500; // Lỗi server
				result.Message = $"Lỗi trong quá trình cập nhật vai trò người dùng: {ex.Message}";
			}

			return result;
		}
		#endregion
		#region đăng ký 
		public async Task<ResponseObject> RegisterAccount(RegisterAccountModel model)
		{
			var result = new PostRequestResponse();
			//check validate
			if (string.IsNullOrWhiteSpace(model.Name))
			{
				result.Status = 400;
				result.Message = "Tên không được để trống!";
				return result;
			}
			if (model.Dob.Year >= 2016)
			{
				result.Status = 400;
				result.Message = "Ngày tháng năm sinh phải trước năm 2016!";
				return result;
			}

			//check confirm password
			if (!string.Equals(model.ConfirmPassword, model.Password))
			{
				result.Status = 400;
				result.Message = "Mật khẩu xác nhận không chính xác!";
				return result;
			}
			if (await _roleManager.RoleExistsAsync("Customer"))
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
					Gender = model.Gender,
					Hight = 0,
					Weight = 0,
					PhoneNumber = model.Phone,
				};
				var resultUser = await _userManager.CreateAsync(user, model.Password);
				// Kiểm tra xem quá trình tạo người dùng có thành công không
				if (resultUser.Succeeded)
				{
					// Gán vai trò "User" cho người dùng
					await _userManager.AddToRoleAsync(user, "Customer");

					//Success
					result.Status = 200;
					result.Message = "Đăng ký thành công, hãy kiểm tra mail của bạn";
					return result;
				}
				else
				{
					result.Status = 400;
					result.Message = "Tạo người dùng thất bại!";
					return result;
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
				return result;
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
					return result;
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

		#region đăng ký bằng SMS
		public async Task<ResponseObject> RegisterAccount(RegisterWithPhoneModel model)
		{
			var result = new PostRequestResponse();

			// Kiểm tra xem email đã tồn tại hay không
			var existingUser = await _userManager.FindByEmailAsync(model.Email);
			if (existingUser != null)
			{
				result.Status = 400;
				result.Message = "Email đã tồn tại!";
				return result;
			}

			// Tạo một đối tượng ApplicationUser từ thông tin trong model
			var user = new ApplicationUser
			{
				Email = model.Email,
				Name = model.Name,
				UserName = model.Email,
				PhoneNumber = model.Phone,
				EmailConfirmed = false // Đặt email chưa xác nhận
			};

			// Tạo tài khoản với email và mật khẩu
			var resultUser = await _userManager.CreateAsync(user, model.Password);

			// Kiểm tra xem quá trình tạo tài khoản có thành công không
			if (resultUser.Succeeded)
			{
				// Gán vai trò "User" cho người dùng
				await _userManager.AddToRoleAsync(user, "User");

				// Tạo mã xác nhận số điện thoại và gửi nó qua SMS
				var phoneNumberToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.Phone);

				// Gửi mã xác nhận qua SMS, bạn cần có một dịch vụ SMS hoặc sử dụng các công nghệ gửi SMS như Twilio
				// Thay 'SendSmsCode' bằng hàm gửi mã xác nhận qua SMS của bạn
				//var smsSent = await SendSmsCode(model.Phone, phoneNumberToken);

				//if (smsSent)
				//{
				//	result.Status = 200;
				//	result.Message = "Đăng ký thành công, mã xác nhận đã được gửi đến số điện thoại của bạn";
				//}
				//else
				//{
				//	result.Status = 500; // Lỗi server
				//	result.Message = "Có lỗi trong quá trình gửi mã xác nhận qua SMS";
				//}
			}
			else
			{
				result.Status = 400;
				result.Message = "Tạo người dùng thất bại!";
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
			const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
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
		#region Cập nhập Tài khoản
		public async Task<ResponseObject> UpdateProfile(UpdateProfile model)
		{
			var result = new PostRequestResponse();
			//check validate
			if (string.IsNullOrWhiteSpace(model.Name))
			{
				result.Status = 400;
				result.Message = "Tên không được để trống và có khoảng trống!";
				return result;
			}
			if (model.Dob.Year >= 2016)
			{
				result.Status = 400;
				result.Message = "Ngày tháng năm sinh phải trước năm 2016!";
				return result;
			}
			if (model.Hight <= 0 || model.Hight >= 300)
			{
				result.Status = 400;
				result.Message = "Chiều cao phải lớn hơn 0 và bé hơn 300 cm!";
				return result;
			}

			if (model.Weight <= 0 || model.Weight >= 200)
			{
				result.Status = 400;
				result.Message = "Cân nặng phải lớn hơn 0 và bé hơn 200 kg!";
				return result;
			}
			var account = await _accountRepository.GetUserByIdAsync(model.Id);
			if (account != null)
			{
				// Cập nhật thông tin tài khoản
				account.Id = account.Id;
				account.Dob = model.Dob;
				account.Name = model.Name;
				account.Hight = model.Hight;
				account.Weight = model.Weight;
				account.Gender = model.Gender;

				// Sử dụng _accountRepository để cập nhật người dùng
				await _accountRepository.UpdateUserAsync(account);

				result.Status = 200;
				result.Message = "Cập nhập người dùng thành công";
				return result;
			}
			return result;
		}

		public async Task<ResponseObject> UpdatePassword(UpdatePasswordModel model)
		{
			var result = new PostRequestResponse();

			// Tìm người dùng bằng ID
			var user = await _userManager.FindByIdAsync(model.Id);

			if (user == null)
			{
				result.Status = 400;
				result.Message = "Người dùng không tồn tại!";
				return result;
			}

			// Kiểm tra mật khẩu cũ
			var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.oldPassword);

			if (!isPasswordValid)
			{
				result.Status = 400;
				result.Message = "Mật khẩu cũ không chính xác!";
				return result;
			}

			// Kiểm tra và đặt lại mật khẩu mới
			if (!Equals(model.passwordConfirm, model.newPassword))
			{
				result.Status = 400;
				result.Message = "Mật xác nhận không chính xác!";
				return result;
			}
			var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
			var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, model.newPassword);

			if (resetResult.Succeeded)
			{
				result.Status = 200;
				result.Message = "Mật khẩu đã được cập nhật";
			}
			else
			{
				result.Status = 400;
				result.Message = "Cập nhật mật khẩu thất bại";
			}

			return result;
		}
		#endregion

		public async Task<ResponseObject> Logout()
		{
			var result = new ResponseObject();

			try
			{
				// Sử dụng _signInManager để đăng xuất người dùng
				await _signInManager.SignOutAsync();

				result.Status = 200;
				result.Message = "Đăng xuất thành công";
				return result;
			}
			catch (Exception ex)
			{
				result.Status = 500; // Lỗi server
				result.Message = $"Lỗi trong quá trình đăng xuất: {ex.Message}";
			}

			return result;
		}


	}
}
