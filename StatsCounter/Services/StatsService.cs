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
            var repositories = await _gitHubService.GetRepositoryInfosByOwnerAsync(owner);

            var letters = new Dictionary<char, int>();

            var names = repositories.Select(x => x.Name.ToLower());
            var namesString = String.Join("", names);
            var disctinctLetters = (from str in names
                                    from c in str
                                    select c).Distinct().ToList();
            disctinctLetters.Sort();

            foreach (var letter in disctinctLetters)
            {
                letters[letter] = namesString.Where(x => x == letter).Count();
            }

            var stats = new RepositoryStats()
            {
                Owner = owner,
                Letters = letters,
                AvgStargazers = repositories.Average(x => x.StargazersCount),
                AvgWatchers = repositories.Average(x => x.WatchersCount),
                AvgSize = repositories.Average(x => x.Size),
                AvgForks = repositories.Average(x => x.ForksCount)
        };
            

            return stats;
        }
    }
}