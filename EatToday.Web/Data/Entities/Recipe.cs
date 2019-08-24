using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Data.Entities
{
    public class Recipe
    {
        public int Id { get; set; }

        [Display(Name = "Name Recipe")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength(500, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Description { get; set; }

        [Display(Name = "Instructions")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Instructions { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<RateRecipe> RateRecipes { get; set; }

        public ICollection<FavouriteRecipe> FavouriteRecipes { get; set; }

        public RecipeType RecipeType { get; set; }


        //TODO: replace the correct URL for the image
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://TDB.azurewebsites.net{ImageUrl.Substring(1)}";
    }
}
