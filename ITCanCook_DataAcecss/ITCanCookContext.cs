using ITCanCook_DataAcecss.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss
{
	public class ITCanCookContext : DbContext
	{

        public ITCanCookContext(DbContextOptions<ITCanCookContext> opt) : base(opt)
        {
            
        }

		#region DBSet
		public DbSet<Recipe>? Recipes { get; set; }
		public DbSet<RecipeAmount>? recipeAmounts { get; set; }
		public DbSet<RecipeCategory>? recipeCategories { get; set; }
		public DbSet<RecipeStep>? recipeSteps { get; set; }
		public DbSet<RecipeStyle>? recipeStyles { get; set; }
		public DbSet<CookingMethod>? cookingMethods { get; set; }
		public DbSet<Ingredient>? ingredients { get; set; }
		public DbSet<IngredientCategory>? IngredientCategories { get; set; }
		#endregion


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			////xác định khóa chính của bảng RecipeAmount
			//modelBuilder.Entity<RecipeAmount>().HasKey(ra => new { ra.RecipeId, ra.IngredientId });


			base.OnModelCreating(modelBuilder);
		}
	}
}
