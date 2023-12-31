﻿using SmallService.Models;
using System.Collections.Generic;

namespace SmallService.Service
{
    public interface ILeaderBoardService
    {
        public decimal Update(long customerId, decimal score);

        public IEnumerable<CustomerRank> Rank(int start, int end);

        public IEnumerable<CustomerRank> RankByCustomerId(long customerId, int high, int low);
    }
}
