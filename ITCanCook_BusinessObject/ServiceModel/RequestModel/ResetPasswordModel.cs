using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Code is required")]
        //[StringLength(6, MinimumLength = 6, ErrorMessage = "Code must be exactly 6 digits")]
        //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Code can only contain letters and digits")]
        public string Code { get; set; } = string.Empty;

    }
}
