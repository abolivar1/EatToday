using EatToday.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Models
{
    public class IngredientViewModel : RecipeIngredient
    {
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Ingredient")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Ingredient.")]
        public int IngredientId { get; set; }
        public IEnumerable<SelectListItem> Ingredients { get; set; }

    }
}
