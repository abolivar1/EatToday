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

        public static string ChooseIngredients => Resource.ChooseIngredients;
        public static string Ingredients => Resource.Ingredients;
        public static string SearchRecipes => Resource.SearchRecipes;
        public static string IngredientsError => Resource.IngredientsError;
        public static string Recipes => Resource.Recipes;
        public static string Recipe => Resource.Recipe;
        public static string Preparation => Resource.Preparation;
        public static string Remarks => Resource.Remarks;
        public static string FirstName => Resource.FirstName;
        public static string LastName => Resource.LastName;
        public static string FirstNamePlaceHolder => Resource.FirstNamePlaceHolder;
        public static string LastNamePlaceHolder => Resource.LastNamePlaceHolder;
        public static string Address => Resource.Address;
        public static string AddressPlaceHolder => Resource.AddressPlaceHolder;
        public static string Phone => Resource.Phone;
        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;
        public static string Save => Resource.Save;
        public static string ChangePass => Resource.ChangePass;
        public static string Profile => Resource.Profile;
        public static string GetIngredients => Resource.GetIngredients;
        public static string Logout => Resource.Logout;
        public static string RecoverPassword => Resource.RecoverPassword;
        public static string Ok => Resource.Ok;





        public static string PasswordConfirm => Resource.PasswordConfirm;
        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;

        //LoginPageViewModel
        public static string ErrorProblem => Resource.ErrorProblem;
        public static string EmailIncorrect => Resource.EmailIncorrect;
        //MapPageViewModel
        public static string Map => Resource.Map;

        //RegisterPageViewModel
        public static string FirstNameError => Resource.FirstNameError;
        public static string LastNameError => Resource.LastNameError;
        public static string AddressError => Resource.AddressError;
        public static string EmailValidError => Resource.EmailValidError;
        public static string PhoneError => Resource.PhoneError;
        public static string PasswordMinimumError => Resource.PasswordMinimumError;
        public static string PasswordConfirmError => Resource.PasswordConfirmError;
        public static string NotMatchPasswordError => Resource.NotMatchPasswordError;

    }

}
