using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Data.Entities
{
    public class FavouriteRecipe
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }

        public Recipe Recipe { get; set; }

        public bool Favourite { get; set; }

    }
}
