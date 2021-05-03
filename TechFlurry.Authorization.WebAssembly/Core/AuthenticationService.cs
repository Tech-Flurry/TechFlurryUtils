using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechFlurry.Authorization.WebAssembly.Abstractions;
using TechFlurry.Authorization.WebAssembly.Common;
using TechFlurry.Authorization.WebAssembly.Infrastructure;
using TechFlurry.Authorization.WebAssembly.Models;

namespace TechFlurry.Authorization.WebAssembly.Core
{
    internal class AuthenticationService : IAuthenticationService
    {
        private const string GRANT_TYPE = "grant_type";
        private const string USERNAME = "username";
        private const string PASSWORD = "password";
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IAuthenticationAPI _authenticationAPI;
        private readonly HttpClient _httpclient;

        public AuthenticationService(IHttpClientProvider httpClientProvider, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage, IAuthenticationAPI authenticationAPI)
        {
            _httpClientProvider = httpClientProvider;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _authenticationAPI = authenticationAPI;
            _httpclient = httpClientProvider.GetHttpClient();
        }
        public async Task<AuthenticatedUser> Login(Credentials credentials)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>(GRANT_TYPE, "password"),
                new KeyValuePair<string, string>(USERNAME,credentials.Username),
                new KeyValuePair<string, string>(PASSWORD,credentials.Password)
            });
            var authResult = await _httpclient.PostAsync(_authenticationAPI.TokenAPIAddress, data);
            var authContent = await authResult.Content.ReadAsStringAsync();
            if (!authResult.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonSerializer.Deserialize<AuthenticatedUser>(authContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            await _localStorage.SetItemAsync(Constants.TOKEN_NAME, result.Access_Token);
            ((AuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(result.Access_Token);
            _httpClientProvider.AddHeader("bearer", result.Access_Token);
            return result;
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(Constants.TOKEN_NAME);
            ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogout();
            _httpClientProvider.RemoveAllHeaders();
        }
    }
}
