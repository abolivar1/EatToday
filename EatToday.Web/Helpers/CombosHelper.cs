using EatToday.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboRecipeTypes()
        {
            //var list = new List<SelectListItem>();

            var list = _dataContext.RecipeTypes.Select(rt => new SelectListItem
            {
                Text = rt.Name,
                Value = $"{rt.Id}"
            })
                .OrderBy(rt => rt.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Select a recipe type...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboIngredients()
        {
            var list = _dataContext.Ingredients.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = $"{i.Id}"
            })
                .OrderBy(i => i.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Select a ingredient...]",
                Value = "0"
            });
            return list;
        }
    }
}
