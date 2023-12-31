using Microsoft.AspNetCore.Mvc;
using SmallService.Service;

namespace SmallService.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILeaderBoardService _leaderBoardService;

        public CustomerController(ILeaderBoardService leaderBoardService) 
        {
            _leaderBoardService = leaderBoardService;
        }

        [HttpGet("customer/{customerId}/score")]
        [HttpGet("customer/{customerId}/score/{score}")]
        public decimal Update(long customerId, decimal score = 0)
        {
            return _leaderBoardService.Update(customerId, score);
        }
    }
}
