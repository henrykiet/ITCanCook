using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Entities
{
	[Table("IngredientCategory")]
	public class IngredientCategory
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string name { get; set; }
        public int IndexDisplay { get; set; }
        public IngredientCategoryStatus Status { get; set; }
        public List<Ingredient> Ingredients { get; set; }
	}
}
