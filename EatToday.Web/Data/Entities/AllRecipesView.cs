using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Data.Entities
{
    public class AllRecipesView
    {

        public int Id { get; set; }
        public string NameRecipe { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
        public string TipoReceta { get; set; }
        public string Amount { get; set; }
        public string NameIngredient { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int Rate { get; set; }





    }
}
