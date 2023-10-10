using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.ResponseModel
{
    public class IngredientResponse
    {
        public int Id { get; set; }
        public int IngredientCategoryId { get; set; }
        public string name { get; set; }
        public string Img { get; set; }
    }
}
