using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EatToday.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboIngredients();
        IEnumerable<SelectListItem> GetComboRecipeTypes();
    }
}