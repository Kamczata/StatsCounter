using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatsCounter.Models;

namespace StatsCounter.Services
{
    public interface IStatsService
    {
        Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner);
    }
    
    public class StatsService : IStatsService
    {
        private readonly IGitHubService _gitHubService;

        public StatsService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner)
        {
            var stats = new RepositoryStats();
            var repositories = await _gitHubService.GetRepositoryInfosByOwnerAsync(owner);
            var letters = new Dictionary<char, int>();

           /* foreach(var repo in repositories)
            {

            }*/

            stats.Owner = owner;
            stats.AvgStargazers = repositories.Average(x => x.StargazersCount);
            stats.AvgWatchers = repositories.Average(x => x.WatchersCount);
            stats.AvgSize = repositories.Average(x => x.Size);
            stats.AvgForks = repositories.Average(x => x.ForksCount);

            return stats;
        }
    }
}