﻿// <auto-generated />
using System;
using ITCanCook_DataAcecss;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ITCanCook_DataAcecss.Migrations
{
    [DbContext(typeof(ITCanCookContext))]
    [Migration("20231001052231_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.CookingMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("cookingMethods");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IngredientCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IngredientCategoryId");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.IngredientCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IngredientCategory");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CookingMethodId")
                        .HasColumnType("int");

                    b.Property<int>("CookingTime")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeStyleId")
                        .HasColumnType("int");

                    b.Property<int>("ServingSize")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CookingMethodId");

                    b.HasIndex("RecipeCategoryId");

                    b.HasIndex("RecipeStyleId");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.RecipeAmount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeAmount");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.RecipeCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DisplayIndex")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RecipeCategory");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.RecipeStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("MediaURl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeStep");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.RecipeStyle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RecipeStyle");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.Ingredient", b =>
                {
                    b.HasOne("ITCanCook_DataAcecss.Entities.IngredientCategory", "IngredientCategory")
                        .WithMany("Ingredients")
                        .HasForeignKey("IngredientCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IngredientCategory");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.Recipe", b =>
                {
                    b.HasOne("ITCanCook_DataAcecss.Entities.CookingMethod", "CookingMethod")
                        .WithMany("Recipes")
                        .HasForeignKey("CookingMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITCanCook_DataAcecss.Entities.RecipeCategory", "RecipeCategory")
                        .WithMany("Recipes")
                        .HasForeignKey("RecipeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITCanCook_DataAcecss.Entities.RecipeStyle", "RecipeStyle")
                        .WithMany("Recipes")
                        .HasForeignKey("RecipeStyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CookingMethod");

                    b.Navigation("RecipeCategory");

                    b.Navigation("RecipeStyle");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.RecipeAmount", b =>
                {
                    b.HasOne("ITCanCook_DataAcecss.Entities.Account", "Account")
                        .WithMany("RecipeAmounts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITCanCook_DataAcecss.Entities.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITCanCook_DataAcecss.Entities.Recipe", "Recipe")
                        .WithMany("Amounts")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.RecipeStep", b =>
                {
                    b.HasOne("ITCanCook_DataAcecss.Entities.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.Account", b =>
                {
                    b.Navigation("RecipeAmounts");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.CookingMethod", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.IngredientCategory", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.Recipe", b =>
                {
                    b.Navigation("Amounts");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.RecipeCategory", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("ITCanCook_DataAcecss.Entities.RecipeStyle", b =>
                {
                    b.Navigation("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}