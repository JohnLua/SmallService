using Microsoft.AspNetCore.Mvc;
using SmallService.Service;
using System;

namespace SmallService.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet("customer/{customerId}/score")]
        [HttpGet("customer/{customerId}/score/{score}")]
        public decimal Update(Int64 customerId, decimal score = 0)
        {
            return MemoryDatabaseService.Update(customerId, score);
        }
    }
}
