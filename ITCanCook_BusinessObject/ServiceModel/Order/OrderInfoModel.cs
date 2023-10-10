using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.Order
{
	public class OrderInfoModel
	{
		public string UserId { get; set; } = string.Empty;
		public TransactionType TransactionType { get; set; }
	}
}
