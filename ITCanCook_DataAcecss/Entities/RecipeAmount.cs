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
        public int Id { get; set; }


        [ForeignKey(nameof(Recipe))]
		public int RecipeId { get; set; }
		[ForeignKey(nameof(Ingredient))]
		public int IngredientId { get; set; }
		[ForeignKey(nameof(Account))]
		public int AccountId { get; set; }

		public string Amount { get; set; }

		public Recipe Recipe { get; set; } 
		public Ingredient Ingredient { get; set; }
		public Account Account { get; set; }

	}
}
