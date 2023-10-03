using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
	public class IngredientCategoryRequest
	{
		[Required]
		public int Id { get; set; }
		public string name { get; set; }
	}
}
