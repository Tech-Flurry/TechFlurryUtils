﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TechFlurry.Authorization.WebAssembly.Abstractions;
using TechFlurry.Authorization.WebAssembly.Core;

namespace TechFlurry.Authorization.WebAssembly.Infrastructure
{
    public static class Setup
    {
        public static IServiceCollection RegisterAuthentication<THttpClientProvider>(this IServiceCollection services) where THttpClientProvider : class, IHttpClientProvider
        {
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            services.AddScoped<IHttpClientProvider, THttpClientProvider>();
            return services;
        }

        public static void RegisterAuthenticationService<TAuthenticationSerivce>(this IServiceCollection services) where TAuthenticationSerivce : class, IAuthenticationService
        {
            services.AddTransient<IAuthenticationService, TAuthenticationSerivce>();
        }

        public static void UseAuthenticationService<TAuthenticationAPI>(this IServiceCollection services) where TAuthenticationAPI : class, IAuthenticationAPI
        {
            services.AddScoped<IAuthenticationAPI, TAuthenticationAPI>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }
    }
}