using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITCanCook_DataAcecss.Enum;

namespace ITCanCook_DataAcecss.Entities
{
	public class Transaction
	{
		[Key]
		public string Id { get; set; }
		[ForeignKey(nameof(ApplicationUser))]
		public string UserId { get; set; } // Tham chiếu đến người dùng thực hiện giao dịch
		public DateTime TransactionDate { get; set; } // Ngày và giờ giao dịch
		public double Amount { get; set; } // Số tiền thanh toán
        public string Notice { get; set; }
        public string TransactionId { get; set; }
        //Ngày hết hạn VIP
        public DateTime EndDate { get; set; }
        // Loại giao dịch (ví dụ: "Mua gói VIP tháng" hoặc "Mua gói VIP năm")
        public TransactionType TransactionType { get; set; }
		public TransactionStatus Status { get; set; } // Giao dịch có thành công hay không

		public ApplicationUser ApplicationUser { get; set; }
	}
}
