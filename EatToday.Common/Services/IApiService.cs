﻿using EatToday.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EatToday.Common.Services
{
    public interface IApiService
    {
        //Task<Response> GetCustomerByEmail(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    string tokenType,
        //    string accessToken,
        //    string email);
        Task<Response> GetRecipesByIngredientsAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int ingredients);

        Task<Response> GetTokenAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            TokenRequest request);
    }

}
