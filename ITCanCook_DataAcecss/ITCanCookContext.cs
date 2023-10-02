using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss
{
    public class ITCanCookContext : IdentityDbContext<ApplicationUser>
	{

        public ITCanCookContext(DbContextOptions<ITCanCookContext> opt) : base(opt)
        {
            
        }

//<<<<<<< HEAD
//		#region
//		public DbSet<RecipeAmount>? Recipes { get; set; }
//=======
		#region DBSet
		public DbSet<Recipe>? Recipes { get; set; }
//>>>>>>> main
		public DbSet<RecipeAmount>? recipeAmounts { get; set; }
		public DbSet<RecipeCategory>? recipeCategories { get; set; }
		public DbSet<RecipeStep>? recipeSteps { get; set; }
		public DbSet<RecipeStyle>? recipeStyles { get; set; }
		public DbSet<CookingMethod>? cookingMethods { get; set; }
		public DbSet<Ingredient>? ingredients { get; set; }
		public DbSet<IngredientCategory>? IngredientCategories { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		#endregion


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			SeedRoles(modelBuilder);
			base.OnModelCreating(modelBuilder);
		}
		private void SeedRoles(ModelBuilder builder)
		{
			builder.Entity<IdentityRole>().HasData
				(
				new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
				new IdentityRole() { Name = "Chef", ConcurrencyStamp = "2", NormalizedName = "Chef" },
				new IdentityRole() { Name = "User", ConcurrencyStamp = "3", NormalizedName = "User" }
				);
		}
	}
}
