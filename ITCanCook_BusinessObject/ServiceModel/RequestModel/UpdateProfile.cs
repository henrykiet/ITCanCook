using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class UpdateProfile
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Hight is required")]
        public float Hight { get; set; }
        [Required(ErrorMessage = "Weight is required")]
        public float Weight { get; set; }
        [Required(ErrorMessage = "Birthday is required")]
        public DateTime Dob { get; set; }
    }
}
