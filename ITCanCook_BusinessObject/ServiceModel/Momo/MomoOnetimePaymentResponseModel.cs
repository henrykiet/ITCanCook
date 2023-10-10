using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.Momo
{
	public class MomoOnetimePaymentResponseModel
	{
        public string userId { get; set; } = string.Empty;
        public string orderId { get; set; } = string.Empty;
        public double amount { get; set; }
        public string transId { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public string orderInfo { get; set; } = string.Empty;
        public TransactionType TransactionType { get; set; }
		public TransactionStatus Status { get; set; }
	}
}
