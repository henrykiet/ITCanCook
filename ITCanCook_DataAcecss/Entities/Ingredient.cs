using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Entities
{
	[Table("Ingredient")]
	public class Ingredient
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey(nameof(IngredientCategory))]
		public int IngredientCategoryId { get; set; }

		public string name { get; set; }
        public string Img { get; set; }

        public IngredientCategory IngredientCategory { get; set;}



	}
}
