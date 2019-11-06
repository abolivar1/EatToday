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
        public static string PasswordError => Resource.PasswordError;
        public static string CheckInternet => Resource.CheckInternet;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string ForgotPassword => Resource.ForgotPassword;
        public static string Email => Resource.Email;

        public static string Login => Resource.Login;
        public static string User => Resource.User;
        public static string Loading => Resource.Loading;

        public static string LoginError => Resource.LoginError;

        public static string Password => Resource.Password;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Register => Resource.Register;

        public static string Rememberme => Resource.Rememberme;
        public static string NotAccount => Resource.NotAccount;
        public static string Rights => Resource.Rights;
    }

}
