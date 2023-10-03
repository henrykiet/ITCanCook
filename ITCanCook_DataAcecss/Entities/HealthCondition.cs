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
	[Table("HealthCondition")]
	public class HealthCondition
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[ForeignKey(nameof(HealthConditionCategory))]
		public int HealthConditionCategoryId { get; set; }
		public string Name { get; set; }
		public bool IsHealthCondition { get; set; }
		public RecipeCategoryStatus Status { get; set; }

		public List<Recipe> Recipes { get; set; }

		public HealthConditionCategory HealthConditionCategory { get; set; }
	}
}
