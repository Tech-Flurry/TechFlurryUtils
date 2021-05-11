using TechFlurry.Authorization.WebAssembly.Abstractions;

namespace TechFlurry.Authorization.WebAssembly.Core
{
    internal class AuthenticationAPI : IAuthenticationAPI
    {
        private readonly string _authAPI;

        public AuthenticationAPI(string authAPI)
        {
            _authAPI = authAPI;
        }
        public string TokenAPIAddress => _authAPI;
    }
}
