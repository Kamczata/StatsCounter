using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StatsCounter.Models;

namespace StatsCounter.Services
{
    public interface IGitHubService
    {
        Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwnerAsync(string owner);
    }
    
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwnerAsync(string owner)
        {

            var url = $"{owner}/repos";
            var response = await _httpClient.GetAsync(url);
            var contentString = await response.Content.ReadAsStringAsync();
            var repoInfo = JsonConvert.DeserializeObject<IEnumerable<RepositoryInfo>>(contentString);
            return repoInfo;

        }
    }
}
