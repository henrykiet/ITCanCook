using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class UpdatePasswordModel
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "Old Password is required")]
        public string oldPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "New Password is required")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()])(?=.{6,})",
        //ErrorMessage = "Password must contain at least 1 uppercase letter, 1 digit, 1 special character, and be at least 6 characters long")]
        public string newPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm Password is required")]

        public string passwordConfirm { get; set; } = string.Empty;
    }
}
