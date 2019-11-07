using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EatToday.Common.Helpers
{
    public static class Settings
    {
        private const string _recipe = "Recipe";
        private const string _recipes = "Recipes";
        private const string _token = "Token";
        private const string _ingredients = "Ingredients";
        private const string _customer = "Customer";
        private const string _isRemembered = "IsRemembered";
        private static readonly bool _boolDefault = false;


        private static readonly string _settingsDefault = string.Empty;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Recipe
        {
            get => AppSettings.GetValueOrDefault(_recipes, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_recipes, value);
        }

        public static string Recipes
        {
            get => AppSettings.GetValueOrDefault(_recipe, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_recipe, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static string Ingredients
        {
            get => AppSettings.GetValueOrDefault(_ingredients, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_ingredients, value);
        }
        public static string Customer
        {
            get => AppSettings.GetValueOrDefault(_customer, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_customer, value);
        }

        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRemembered, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemembered, value);
        }
    }

}
