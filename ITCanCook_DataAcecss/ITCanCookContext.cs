using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class ITCanCookContext : IdentityDbContext<ApplicationUser>
{

	public ITCanCookContext(DbContextOptions<ITCanCookContext> opt) : base(opt)
	{

	}

	#region DBSet
	public DbSet<Recipe>? Recipes { get; set; }
	public DbSet<RecipeAmount>? recipeAmounts { get; set; }
	public DbSet<HealthCondition>? recipeCategories { get; set; }
	public DbSet<RecipeStep>? recipeSteps { get; set; }
	public DbSet<Equipment>? recipeStyles { get; set; }
	public DbSet<Ingredient>? ingredients { get; set; }
	public DbSet<IngredientCategory>? IngredientCategories { get; set; }
	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	public DbSet<CookingHobby> CookingHobbies { get; set; }
	public DbSet<HealthConditionCategory> HealthConditionCategories { get; set; }
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