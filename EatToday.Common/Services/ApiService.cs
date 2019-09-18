using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EatToday.Common.Models;
using Newtonsoft.Json;


namespace EatToday.Common.Services
{
    public class ApiService : IApiService
    {
        public async Task<Response> GetTokenAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            TokenRequest request)
        {
            try
            {
                var requestString = JsonConvert.SerializeObject(request);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var token = JsonConvert.DeserializeObject<TokenResponse>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = token
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> GetRecipesByIngredientsAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int ingredients)
        {
            try
            {
                var request = new RecipeRequest { Ingredients = ingredients };
                var requestString = JsonConvert.SerializeObject(request);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var recipes = JsonConvert.DeserializeObject<RecipeResponse>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = recipes
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        //public async Task<Response> GetCustomerByEmail(
        //    string urlBase,
        //    string servicePrefix,
        //    string controller,
        //    string tokenType,
        //    string accessToken,
        //    string email)
        //{
        //    try
        //    {
        //        var request = new EmailRequest { Email = email };
        //        var requestString = JsonConvert.SerializeObject(request);
        //        var content = new StringContent(requestString, Encoding.UTF8, "application/json");
        //        var client = new HttpClient
        //        {
        //            BaseAddress = new Uri(urlBase)
        //        };

        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
        //        var url = $"{servicePrefix}{controller}";
        //        var response = await client.PostAsync(url, content);
        //        var result = await response.Content.ReadAsStringAsync();

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = result,
        //            };
        //        }

        //        var customer = JsonConvert.DeserializeObject<OwnerResponse>(result);
        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Result = customer
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message
        //        };
        //    }
        //}
    }

}
