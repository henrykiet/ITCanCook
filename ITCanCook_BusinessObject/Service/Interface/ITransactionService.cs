using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.ServiceModel.Momo;
using ITCanCook_BusinessObject.ServiceModel.Order;
using ITCanCook_DataAcecss.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
	public interface ITransactionService
	{
		Task<MomoCreatePaymentResquestModel> CreatePaymentAsync(OrderInfoModel model);
		Task<ResponseObject> MomoPaymentReturn(MomoOnetimePaymentResponseModel model);
		public Task<string> ScheduleHangfireJobToUpdateIsPremium(string userId);
	}
}
