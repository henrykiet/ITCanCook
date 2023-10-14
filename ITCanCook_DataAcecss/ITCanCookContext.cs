using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

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
	public DbSet<Transaction> Transactions { get; set; }
	#endregion


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		SeedRoles(modelBuilder);
	}

	private void SeedRoles(ModelBuilder builder)
	{
		// Always create the roles
		builder.Entity<IdentityRole>().HasData
		(
			new IdentityRole() { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
			new IdentityRole() { Id = "2", Name = "Prenium", NormalizedName = "PRENIUM" },
			new IdentityRole() { Id = "3", Name = "Customer", NormalizedName = "CUSTOMER" }
		);

		// Create the admin user and assign the "Admin" role
		var adminUser = new ApplicationUser
		{
			UserName = "admin@gmail.com",
			Email = "admin@gmail.com",
			NormalizedEmail = "ADMIN@GMAIL.COM",
			NormalizedUserName = "ADMIN@GMAIL.COM",
			EmailConfirmed = true,
			Name = "admin",
			LockoutEnabled = true,
			// Add any other properties you want to set for the user
		};

		var password = "Admin123@";

		builder.Entity<ApplicationUser>().HasData(adminUser);

		var hasher = new PasswordHasher<ApplicationUser>();
		adminUser.PasswordHash = hasher.HashPassword(adminUser, password);

		builder.Entity<IdentityUserRole<string>>().HasData(
			new IdentityUserRole<string>
			{
				RoleId = "1", // "Admin" role
				UserId = adminUser.Id
			}
		);
	}
}