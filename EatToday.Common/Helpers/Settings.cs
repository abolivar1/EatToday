﻿using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EatToday.Common.Helpers
{
    public static class Settings
    {
        private const string _recipe = "Recipe";
        private const string _token = "Token";

        private static readonly string _settingsDefault = string.Empty;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Recipe
        {
            get => AppSettings.GetValueOrDefault(_recipe, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_recipe, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }
    }

}
