using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public ICollection<RateRecipe> RateRecipes { get; set; }

        public ICollection<FavouriteRecipe> FavouriteRecipes { get; set; }


    }
}
