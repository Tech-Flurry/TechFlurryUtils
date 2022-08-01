using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechFlurry.Utils.MetronicComponents.Interops;

namespace TechFlurry.Utils.MetronicComponents.Infrastructure
{
    public static class Setup
    {
        public static void UseMetronicComponents(this IServiceCollection services, string baseUrl)
        {
            if (baseUrl.ToLower().StartsWith("config:"))
            {
                services.AddSingleton(x =>
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

            //
            services.AddTransient<IDateTimeRangeInterop, DateTimeRangeInterop>();
            services.AddTransient<IMaskedInputInterop, MaskedInputInterop>();
            services.AddTransient<ISelect2Interop, Select2Interop>();
        }
    }
}