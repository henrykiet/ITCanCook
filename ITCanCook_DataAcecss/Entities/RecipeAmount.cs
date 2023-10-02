using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Entities
{
	[Table("RecipeAmount")]
	public class RecipeAmount
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }


        [ForeignKey(nameof(Recipe))]
		public int RecipeId { get; set; }
		[ForeignKey(nameof(Ingredient))]
		public int IngredientId { get; set; }

		public string Amount { get; set; }

		public RecipeAmount Recipe { get; set; } 
		public Ingredient Ingredient { get; set; }

	}
}
