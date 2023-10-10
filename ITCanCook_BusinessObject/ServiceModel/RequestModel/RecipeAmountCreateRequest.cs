using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class RecipeAmountCreateRequest
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string Amount { get; set; }
    }
}
