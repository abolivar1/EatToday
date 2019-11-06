using EatToday.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<RateRecipe> RateRecipes { get; set; }

        public DbSet<FavouriteRecipe> FavouriteRecipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<RecipeType> RecipeTypes { get; set; }

        public DbSet<AllRecipesView> AllRecipesViews { get; set; }


    }
}
