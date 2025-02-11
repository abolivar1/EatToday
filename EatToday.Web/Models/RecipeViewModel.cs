﻿using EatToday.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Models
{
    public class RecipeViewModel : Recipe
    {

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Recipe Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a recipe type.")]
        public int RecipeTypeId { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        public IEnumerable<SelectListItem> RecipeTypes { get; set; }
        public IEnumerable<SelectListItem> Ingredients { get; set; }

    }

}
