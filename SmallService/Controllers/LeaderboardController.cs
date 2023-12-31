using Microsoft.AspNetCore.Mvc;
using SmallService.Models;
using SmallService.Service;
using System.Collections.Generic;

namespace SmallService.Controllers
{
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderBoardService _leaderboardService;

        public LeaderboardController(ILeaderBoardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        [HttpGet("leaderboard")]
        public IEnumerable<CustomerRank> Rank(int start, int end)
        {
            return _leaderboardService.Rank(start, end);
        }

        [HttpGet("leaderboard/{customerId}")]
        public IEnumerable<CustomerRank> RankByCustomerId(long customerId, int high = 0, int low = 0)
        {
            return _leaderboardService.RankByCustomerId(customerId, high, low);
        }
    }
}
