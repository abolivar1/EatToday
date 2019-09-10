using System;
using System.Collections.Generic;
using System.Text;

namespace EatToday.Common.Models
{
    public class RecipeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
        public string RecipeType { get; set; }
        public ICollection<IngredientRecipeResponse> IngredientRecipes { get; set; }
        public ICollection<CommentResponse> CommentResponses { get; set; }


    }
}
