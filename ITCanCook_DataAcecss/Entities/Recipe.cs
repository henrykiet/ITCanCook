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
        public string Name { get; set; }

		[ForeignKey(nameof(Equipment))]
		public int EquipmentId { get; set; }
		[ForeignKey(nameof(HealthCondition))]
		public int HealthConditionId { get; set; }
		[ForeignKey(nameof(CookingHobby))]
		public int CookingHobbyId { get; set; }

		public string ImgLink { get; set; }
		public int CookingTime { get; set; }
		public string? Description { get; set; }
		public int ServingSize { get; set; }
		public int Energy { get;set; }
		public string Meals { get; set; }


		public List<RecipeStep> Steps { get; set; }
		public List<RecipeAmount> Amounts { get; set; }


		public HealthCondition HealthCondition { get; set; }
		public Equipment Equipment { get; set; }
		public CookingHobby CookingHobby { get; set; }
	}
}
