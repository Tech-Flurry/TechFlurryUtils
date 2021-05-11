using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TechFlurry.Authorization.WebAssembly.Abstractions;
using TechFlurry.Authorization.WebAssembly.Common;

namespace TechFlurry.Authorization.WebAssembly.Infrastructure
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState _anonymous;
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly ILocalStorageService _localStorage;


        public AuthStateProvider(IHttpClientProvider httpClientProvider, ILocalStorageService localStorage)
        {
            _httpClientProvider = httpClientProvider;
            _localStorage = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal());
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(Constants.TOKEN_NAME);
            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return _anonymous;
            }
            _httpClientProvider.AddHeader("bearer", token);
            NotifyUserAuthentication(token);
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
            var claims = JwtParser.ParseClaimsFromJwt(token);
            DateTime utcNow = DateTime.UtcNow;
            // Checks the nbf field of the token
            var notValidBefore = claims.Where(x => x.Type.Equals("nbf")).FirstOrDefault();
            if (notValidBefore is not null)
            {
                var datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(notValidBefore.Value));
                if (datetime.UtcDateTime > utcNow)
                    return new ClaimsPrincipal();
            }
            // Checks the exp field of the token
            var expiry = claims.Where(claim => claim.Type.Equals("exp")).FirstOrDefault();
            if (expiry is not null)
            {
                // The exp field is in Unix time
                var datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry.Value));
                if (datetime.UtcDateTime <= utcNow)
                    return new ClaimsPrincipal();
            }
            return new ClaimsPrincipal(new ClaimsIdentity(claims, Constants.AUTH_TYPE));
        }
    }
}