using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EatToday.Common.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace EatToday.Common.Services
{
    public class ApiService : IApiService
    {
        public async Task<bool> CheckConnection(string url)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return false;
            }

            return await CrossConnectivity.Current.IsRemoteReachable(url);
        }


        public async Task<Response<TokenResponse>> GetTokenAsync(
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
                    return new Response<TokenResponse>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var token = JsonConvert.DeserializeObject<TokenResponse>(result);
                return new Response<TokenResponse>
                {
                    IsSuccess = true,
                    Result = token
                };
            }
            catch (Exception ex)
            {
                return new Response<TokenResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<RecipeResponse>> GetRecipesByIngredientsAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string ingredients)
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
                    return new Response<RecipeResponse>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var recipes = JsonConvert.DeserializeObject<RecipeResponse>(result);
                return new Response<RecipeResponse>
                {
                    IsSuccess = true,
                    Result = recipes
                };
            }
            catch (Exception ex)
            {
                return new Response<RecipeResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<IngredientResponse>> GetIngredientsAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{servicePrefix}{controller}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<IngredientResponse>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                var ingredients = JsonConvert.DeserializeObject<List<IngredientResponse>>(result);
                return new Response<IngredientResponse>
                {
                    IsSuccess = true,
                    ResultList = ingredients
                };
            }
            catch (Exception ex)
            {
                return new Response<IngredientResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }

}
