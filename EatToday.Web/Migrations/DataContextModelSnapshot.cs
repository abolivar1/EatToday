﻿// <auto-generated />
using System;
using EatToday.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EatToday.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EatToday.Web.Data.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("RecipeId");

                    b.Property<string>("Remarks")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.FavouriteRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId");

                    b.Property<bool>("Favourite");

                    b.Property<int?>("RecipeId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RecipeId");

                    b.ToTable("FavouriteRecipes");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.RateRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId");

                    b.Property<int>("Rate");

                    b.Property<int?>("RecipeId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RateRecipes");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Instructions")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("RecipeTypeId");

                    b.HasKey("Id");

                    b.HasIndex("RecipeTypeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.RecipeIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount")
                        .HasMaxLength(20);

                    b.Property<int?>("IngredientId");

                    b.Property<int?>("RecipeId");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.RecipeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("RecipeTypes");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.Comment", b =>
                {
                    b.HasOne("EatToday.Web.Data.Entities.Customer", "Customer")
                        .WithMany("Comments")
                        .HasForeignKey("CustomerId");

                    b.HasOne("EatToday.Web.Data.Entities.Recipe", "Recipe")
                        .WithMany("Comments")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.FavouriteRecipe", b =>
                {
                    b.HasOne("EatToday.Web.Data.Entities.Customer", "Customer")
                        .WithMany("FavouriteRecipes")
                        .HasForeignKey("CustomerId");

                    b.HasOne("EatToday.Web.Data.Entities.Recipe", "Recipe")
                        .WithMany("FavouriteRecipes")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.RateRecipe", b =>
                {
                    b.HasOne("EatToday.Web.Data.Entities.Customer", "Customer")
                        .WithMany("RateRecipes")
                        .HasForeignKey("CustomerId");

                    b.HasOne("EatToday.Web.Data.Entities.Recipe", "Recipe")
                        .WithMany("RateRecipes")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.Recipe", b =>
                {
                    b.HasOne("EatToday.Web.Data.Entities.RecipeType", "RecipeType")
                        .WithMany("Recipes")
                        .HasForeignKey("RecipeTypeId");
                });

            modelBuilder.Entity("EatToday.Web.Data.Entities.RecipeIngredient", b =>
                {
                    b.HasOne("EatToday.Web.Data.Entities.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId");

                    b.HasOne("EatToday.Web.Data.Entities.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId");
                });
#pragma warning restore 612, 618
        }
    }
}
