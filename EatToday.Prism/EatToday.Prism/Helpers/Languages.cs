using EatToday.Prism.Interfaces;
using EatToday.Prism.Resources;

using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace EatToday.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string EmailError => Resource.EmailError;
    }

}
