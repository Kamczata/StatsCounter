using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using StatsCounter.Services;

namespace StatsCounter.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGitHubService(
            this IServiceCollection services, Uri baseApiUrl)
        {
            services.AddHttpClient<IGitHubService, GitHubService>(x =>
            {
                x.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("github", "1"));
                x.BaseAddress = baseApiUrl;
            }
            ); 
            return services;

        }

        
    }
}