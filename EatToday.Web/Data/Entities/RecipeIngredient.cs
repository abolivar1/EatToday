using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Data.Entities
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [Display(Name = "Amount")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Amount { get; set; }

        public Ingredient Ingredient { get; set; }

        public Recipe Recipe { get; set; }
    }
}
