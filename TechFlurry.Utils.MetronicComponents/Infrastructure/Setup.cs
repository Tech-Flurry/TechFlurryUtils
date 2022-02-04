using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechFlurry.Utils.MetronicComponents.Interops;

namespace TechFlurry.Utils.MetronicComponents.Infrastructure
{
    public static class Setup
    {
        public static void UseMetronicServerComponents(this IServiceCollection services, string baseUrl)
        {
            if (baseUrl.ToLower().StartsWith("config:"))
            {
                services.AddSingleton<ApplicationInfo>(x =>
                {
                    var config = x.GetService<IConfiguration>();
                    var baseUrlValue = config.GetValue<string>(baseUrl.ToLower().Replace("config:", string.Empty));
                    return new ApplicationInfo(baseUrlValue);
                });
            }
            else
            {
                services.AddSingleton(new ApplicationInfo(baseUrl));
            }
            services.AddScoped<ICommonFunctions, CommonFunctions>();
            services.AddScoped<IGlobalSearchInterop, GlobalSearchInterop>();
        }
    }
}