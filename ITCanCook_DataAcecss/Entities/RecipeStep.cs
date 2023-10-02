using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Entities
{
	[Table("RecipeStep")]
	public class RecipeStep
	{
		[Key]
		public int Id { get; set; }


		[ForeignKey(nameof(Recipe))]
		public int RecipeId { get; set; }

		public int Index { get; set; }
		public string MediaURl { get; set; }
		public string? Description { get; set; }

		public Recipe Recipe { get; set; }
	}
}
