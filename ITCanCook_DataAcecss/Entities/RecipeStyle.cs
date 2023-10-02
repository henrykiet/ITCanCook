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
	[Table("RecipeStyle")]
	public class RecipeStyle
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public RecipeStyleStatus Status { get; set; }
		public List<RecipeAmount> Recipes { get; set; }

	}
}
