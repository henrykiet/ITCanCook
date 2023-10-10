using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.Momo;
using ITCanCook_BusinessObject.ServiceModel.Order;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Enum;
using ITCanCook_DataAcecss.Repository.Implement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Hangfire;

namespace ITCanCook_BusinessObject.Service.Implement
{
	public class TransactionService : ITransactionService
	{
		private readonly ITransactionRepository _transactionRepositopry;
		private readonly IOptions<MomoOptionModel> _options;
		private readonly UserManager<ApplicationUser> _userManager;

		public TransactionService(ITransactionRepository transactionRepository, IOptions<MomoOptionModel> options,
			UserManager<ApplicationUser> userManager)
		{
			_transactionRepositopry = transactionRepository;
			_options = options;
			_userManager = userManager;
		}
		#region Momo
		public async Task<MomoCreatePaymentResquestModel> CreatePaymentAsync(OrderInfoModel model)
		{
			//tạo IdOrder
			var orderId = Guid.NewGuid();
			//lấy userId
			var userId = model.UserId;
			var user = _userManager.FindByIdAsync(userId).Result;
			//gói dịch vụ và giá
			var transactionType = string.Empty;
			var type = TransactionType.PRENIUM_WEEK;
			double amount = 0;
			switch (model.TransactionType)
			{
				case TransactionType.PRENIUM_WEEK:
					transactionType = "Prinium 1 tuần";
					type = TransactionType.PRENIUM_WEEK;
					amount = 5000;
					break;
				case TransactionType.PRENIUM_MONTH:
					transactionType = "Prinium 1 tháng";
					type = TransactionType.PRENIUM_MONTH;
					amount = 18000;
					break;
				case TransactionType.PRENIUM_HALFYEAR:
					transactionType = "Prinium 6 tháng";
					type = TransactionType.PRENIUM_HALFYEAR;
					amount = 90000;
					break;
				case TransactionType.PRENIUM_YEAR:
					transactionType = "Prinium 1 năm";
					type = TransactionType.PRENIUM_YEAR;
					amount = 160000;
					break;
			}


			var orderInfo = "Khách hàng: " + user.Name + "Đăng ký gói: " + transactionType;
			var rawData =
				$"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}" +
				$"&requestId={userId}&amount={amount}&orderId={orderId}&orderInfo={orderInfo}" +
				$"&returnUrl={_options.Value.ReturnUrl}&notifyUrl={_options.Value.NotifyUrl}" +
				$"&extraData={type.ToString()}";

			var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

			var client = new RestClient(_options.Value.MomoApiUrl);
			var request = new RestRequest() { Method = Method.Post };
			request.AddHeader("Content-Type", "application/json; charset=UTF-8");

			// Create an object representing the request data
			var requestData = new
			{
				accessKey = _options.Value.AccessKey,
				partnerCode = _options.Value.PartnerCode,
				requestType = _options.Value.RequestType,
				notifyUrl = _options.Value.NotifyUrl,
				returnUrl = _options.Value.ReturnUrl,
				orderId = orderId,
				amount = amount.ToString(),
				orderInfo = orderInfo,
				requestId = userId,
				extraData = type.ToString(),
				signature = signature,
			};

			request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

			var response = await client.ExecuteAsync(request);

			return JsonConvert.DeserializeObject<MomoCreatePaymentResquestModel>(response.Content);
		}

		private string ComputeHmacSha256(string message, string secretKey)
		{
			var keyBytes = Encoding.UTF8.GetBytes(secretKey);
			var messageBytes = Encoding.UTF8.GetBytes(message);

			byte[] hashBytes;

			using (var hmac = new HMACSHA256(keyBytes))
			{
				hashBytes = hmac.ComputeHash(messageBytes);
			}

			var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

			return hashString;
		}

		public async Task<ResponseObject> MomoPaymentReturn(MomoOnetimePaymentResponseModel model)
		{
			var result = new ResponseObject();
			try
			{
				// Kiểm tra xem id có tồn tại hay không
				var userExist = _userManager.FindByIdAsync(model.userId).Result;
				if (userExist != null && model.Status == TransactionStatus.PAID)
				{
					// Tạo giao dịch
					var transaction = new Transaction
					{
						Id = model.orderId,
						UserId = model.userId,
						Notice = model.orderInfo,
						Amount = model.amount,
						TransactionDate = model.TransactionDate,
						TransactionId = model.transId,
						Status = model.Status,
						TransactionType = model.TransactionType
					};
					// Xác định EndDate dựa trên TransactionType
					switch (model.TransactionType)
					{
						case TransactionType.PRENIUM_WEEK:
							// Nếu TransactionType là PRENIUM_WEEK, thì EndDate sẽ là ngày hiện tại cộng thêm 1 tuần
							transaction.EndDate = DateTime.Now.AddDays(7);
							break;
						case TransactionType.PRENIUM_MONTH:
							// Tương tự, bạn có thể xử lý cho các trường hợp khác ở đây
							transaction.EndDate = DateTime.Now.AddMonths(1);
							break;
						case TransactionType.PRENIUM_HALFYEAR:
							transaction.EndDate = DateTime.Now.AddMonths(6);
							break;
						case TransactionType.PRENIUM_YEAR:
							transaction.EndDate = DateTime.Now.AddYears(1);
							break;
						default:
							// Xử lý trường hợp không xác định
							transaction.EndDate = DateTime.Now; // Đặt EndDate thành ngày hiện tại
							break;
					}

					// Kiểm tra trạng thái của giao dịch
					var newTransaction = _transactionRepositopry.Insert(transaction);
					if (newTransaction)
					{
						if (await UpdateIsPremiumIfPaidAsync(transaction.Id, transaction.UserId))
						{
							// Giao dịch thành công
							result.Status = 200;
							result.Message = "Giao dịch thành công.";

							// Đặt công việc Hangfire để cập nhật IsPrenium thành false sau khi hết hạn
							var jobId = BackgroundJob.Schedule(()
								=> UpdateIsPremiumToFalseAsync(transaction.UserId), transaction.EndDate);
						}
					}
					else
					{
						// Giao dịch không thành công
						result.Status = 400;
						result.Message = "Giao dịch không thành công.";
					}
				}
				else
				{
					// User không tồn tại
					result.Status = 400;
					result.Message = "User doesn't exist!";
				}

			}
			catch (Exception)
			{
				// Xử lý ngoại lệ nếu có lỗi xảy ra
				result.Status = 500; // Hoặc mã lỗi nếu có lỗi xảy ra
				result.Message = "Có lỗi xảy ra trong quá trình xử lý giao dịch.";
				// Log ngoại lệ ex tại đây nếu cần
			}

			return result;
		}


		#endregion


		#region IsPrenium

		private async Task<bool> UpdateIsPremiumIfPaidAsync(string orderId, string userId)
		{
			// Kiểm tra trạng thái của giao dịch
			var transaction = _transactionRepositopry.GetByIdB(orderId);

			if (transaction != null && transaction.Status == TransactionStatus.PAID)
			{
				// Tìm người dùng theo userId
				var user = await _userManager.FindByIdAsync(userId);

				if (user != null)
				{
					// Cập nhật trường IsPremium thành true
					user.IsPrenium = true;

					// Lưu thay đổi người dùng
					var result = await _userManager.UpdateAsync(user);

					if (result.Succeeded)
					{
						return true; // Cập nhật thành công
					}
					else
					{
						// Xử lý lỗi cập nhật người dùng nếu cần
						return false;
					}
				}
			}

			return false; // Giao dịch không phải là PAID hoặc không tìm thấy người dùng
		}

		public async Task UpdateIsPremiumToFalseAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				user.IsPrenium = false;
				await _userManager.UpdateAsync(user);
			}
		}
		//hàm test hangfire
		public async Task<string> ScheduleHangfireJobToUpdateIsPremium(string userId)
		{
			// Đặt công việc Hangfire để cập nhật IsPrenium thành false sau 20 giây
			var jobId = BackgroundJob.Schedule(() => UpdateIsPremiumToFalseAsync(userId), TimeSpan.FromSeconds(20));

			// Trả về jobId cho người dùng hoặc lưu vào cơ sở dữ liệu tùy theo nhu cầu
			return jobId;
		}




		#endregion
	}
}
