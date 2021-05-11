using System.Threading.Tasks;
using TechFlurry.Authorization.WebAssembly.Models;

namespace TechFlurry.Authorization.WebAssembly.Abstractions
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUser> Login(Credentials credentials);
        Task Logout();
    }
}