using SmallService.Model;
using System;
using System.Collections.Generic;

namespace SmallService.Service
{
    public interface ILeaderboardService
    {
        IEnumerable<Customer> Range(int start, int end);

        IEnumerable<Customer> Range(Int64 customerId, int high, int low);
    }
}
