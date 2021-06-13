using Microsoft.Extensions.DependencyInjection;
using TechFlurry.Utils.MetronicComponents.Interops;

namespace TechFlurry.Utils.MetronicComponents.Infrastructure
{
    public static class Setup
    {
        public static void UseMetronicServerComponents(this IServiceCollection services)
        {
            services.AddScoped<ICommonFunctions, CommonFunctions>();
            services.AddScoped<IGlobalSearchInterop, GlobalSearchInterop>();
        }
    }
}