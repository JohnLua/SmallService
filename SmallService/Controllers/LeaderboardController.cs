using Microsoft.AspNetCore.Mvc;
using SmallService.Model;
using SmallService.Service;
using System;
using System.Collections.Generic;

namespace SmallService.Controllers
{
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        [HttpGet("leaderboard")]
        public IEnumerable<Customer> Range(int start, int end)
        {
            return MemoryDatabaseService.Range(start, end);
        }


        [HttpGet("leaderboard/{customerId}")]
        public IEnumerable<Customer> RangeByCustomerId(Int64 customerId, int high, int low)
        {
            return MemoryDatabaseService.RangeByCustomerId(customerId, high, low);
        }
    }
}
