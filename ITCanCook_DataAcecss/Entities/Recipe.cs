using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Entities
{
	[Table("Recipe")]
	public class Recipe
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }


		[ForeignKey(nameof(RecipeCategory))]
		public int RecipeCategoryId { get; set; }
		[ForeignKey(nameof(RecipeStyle))]
		public int RecipeStyleId { get; set; }
		[ForeignKey(nameof(CookingMethod))]
		public int CookingMethodId { get; set; }

		public string ImgLink { get; set; }
		public DateTime CookingTime { get; set; }
		public string? Description { get; set; }
		public int ServingSize { get; set; }
		
		public List<RecipeStep> Steps { get; set; }
		public List<RecipeAmount> Amounts { get; set; }


		public RecipeCategory RecipeCategory { get; set; }
		public RecipeStyle RecipeStyle { get; set; }
		public CookingMethod CookingMethod { get; set; }


	}
}
