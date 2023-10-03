using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
	public class IngredientCategoryCreateRequest
	{
		//public int Id { get; set; }
		public string name { get; set; }
		public IngredientCategoryStatus Satuts { get; set; }
	}
}
