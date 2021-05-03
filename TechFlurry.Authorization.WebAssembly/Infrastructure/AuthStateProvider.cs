using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using TechFlurry.Authorization.WebAssembly.Abstractions;
using TechFlurry.Authorization.WebAssembly.Common;

namespace TechFlurry.Authorization.WebAssembly.Infrastructure
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState _anonymous;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly ILocalStorageService _localStorage;
        public AuthStateProvider(IHttpClientProvider httpClientProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClientProvider.GetHttpClient();
            _httpClientProvider = httpClientProvider;
            _localStorage = localStorage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(Constants.TOKEN_NAME);
            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return _anonymous;
            }
            _httpClientProvider.AddHeader("bearer", token);
            return new AuthenticationState(GetAuthenticatedUser(token));
        }

        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = GetAuthenticatedUser(token);
            var authenticationState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authenticationState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }

        internal static ClaimsPrincipal GetAuthenticatedUser(string token)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), Constants.AUTH_TYPE));
        }
    }
}