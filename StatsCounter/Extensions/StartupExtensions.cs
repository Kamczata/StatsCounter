using System;
using Microsoft.Extensions.DependencyInjection;
using StatsCounter.Services;

namespace StatsCounter.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGitHubService(
            this IServiceCollection services, Uri baseApiUrl)
        {
            services.AddTransient<IGitHubService, GitHubService>().AddHttpClient("Guthub", x => x.BaseAddress = baseApiUrl);
            return services;

        }

        
    }
}