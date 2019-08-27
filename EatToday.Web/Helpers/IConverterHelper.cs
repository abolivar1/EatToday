using System.Threading.Tasks;
using EatToday.Web.Data.Entities;
using EatToday.Web.Models;

namespace EatToday.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Recipe> ToRecipeAsync(RecipeViewModel model, string path);
    }
}