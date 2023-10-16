using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.Momo;
using ITCanCook_BusinessObject.ServiceModel.Order;
using ITCanCook_DataAcecss.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITCanCook.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly ITransactionService _momoService;

		public TransactionController(ITransactionService momoService)
		{
			_momoService = momoService;
		}

		[HttpPost("CreatePayment")]
		public async Task<IActionResult> CreatePayment( OrderInfoModel model)
		{
			var response = await _momoService.CreatePaymentAsync(model);
			// Trả về dữ liệu JSON hoặc URL tới client
			return Ok(new { PayUrl = response.PayUrl });
		}

		[HttpGet("PaymentCallback")]
		public async Task<IActionResult> PaymentCallback()
		{
			var result = new ResponseObject();

			// Đọc các tham số từ query string
			var errorCode = HttpContext.Request.Query["errorCode"];
			var message = HttpContext.Request.Query["message"];

			var orderId = HttpContext.Request.Query["orderId"];
			var requestId = HttpContext.Request.Query["requestId"];
			var orderInfo = HttpContext.Request.Query["orderInfo"];
			var amountString = HttpContext.Request.Query["amount"];
			var responseTime = HttpContext.Request.Query["responseTime"];
			var tranId = HttpContext.Request.Query["transId"];
			var extraData = HttpContext.Request.Query["extraData"];

			if (errorCode == "0")
			{
				// Xác định TransactionStatus dựa vào errorCode và message
				TransactionStatus transactionStatus = TransactionStatus.PENDING; // Giả định mặc định là PENDING

				if (errorCode == "0" && message == "Success")
				{
					transactionStatus = TransactionStatus.PAID;
				}
				else if (errorCode != "0")
				{
					transactionStatus = TransactionStatus.UNPAID;
				}

				// Chuyển đổi giá trị amount từ chuỗi sang kiểu dữ liệu double
				double amount;
				if (!double.TryParse(amountString, out amount))
				{
					// Xử lý lỗi chuyển đổi nếu cần
					return BadRequest("Không thể chuyển đổi giá trị amount.");
				}

				// Chuyển đổi giá trị extraData thành kiểu dữ liệu TransactionType
				TransactionType transactionType;
				if (!Enum.TryParse(extraData, out transactionType))
				{
					// Xử lý lỗi chuyển đổi nếu cần
					return BadRequest("Không thể chuyển đổi giá trị extraData.");
				}

				// Xây dựng MomoOnetimePaymentResponseModel với các giá trị đã chuyển đổi
				var model = new MomoOnetimePaymentResponseModel
				{
					orderId = orderId,
					userId = requestId,
					orderInfo = orderInfo,
					amount = amount,
					TransactionDate = DateTime.Parse(responseTime),
					TransactionType = transactionType,
					Status = transactionStatus,
					transId = tranId
				};

				result = await _momoService.MomoPaymentReturn(model);
				if (result.Status == 200)
				{
					result.Status = 200;
					result.Message = "Payment successfully";
				}
			}
			else
			{
				// Xử lý lỗi nếu có lỗi từ Momo (errorCode khác 0)
				result.Status = 400; // Hoặc mã lỗi của bạn
				result.Message = "Lỗi giao dịch Momo: " + message;
			}

			var statusCode = (int)result.Status;
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};
			return jsonResult;
		}
		//[Authorize]
		[HttpGet("CheckAndUpdateIsPremium")]
		public async Task<IActionResult> CheckAndUpdateIsPremium(TransactionStatus status, string userId, TransactionType type, double amount)
		{
			// Gọi phương thức của service để cập nhật trạng thái IsPrenium của người dùng
			var result = await _momoService.ScheduleHangfireJobToUpdateIsPremium(status, userId, type, amount);

			// Trả về kết quả
			var statusCode = (int)result.Status;
			var jsonResult = new JsonResult(result)
			{
				StatusCode = statusCode
			};
			return jsonResult;
		}


	}
}
