using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class AccountModel
    {
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime Dob { get; set; }
        public Gender Gender { get; set; }
        public float Hight { get; set; }
        public float Weight { get; set; }
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ. Vui lòng nhập 10 hoặc 11 chữ số và bắt đầu từ số 0.")]
        public string Phone { get; set; } = string.Empty;


    }
}
