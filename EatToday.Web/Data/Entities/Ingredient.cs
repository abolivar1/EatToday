using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Data.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Display(Name = "Ingredient Name")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
