using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Data.Entities
{
    public class RateRecipe
    {
        public int Id { get; set; }

        [Display(Name = "Rate")]
        public int Rate { get; set; }

        public Customer Customer { get; set; }

        public Recipe Recipe { get; set; }
    }
}
