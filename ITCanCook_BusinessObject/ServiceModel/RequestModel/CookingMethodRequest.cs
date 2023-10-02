using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class CookingMethodRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public CookingMethodStatus Status { get; set; }
        //public List<RecipeAmount> Recipes { get; set; }
    }
}
