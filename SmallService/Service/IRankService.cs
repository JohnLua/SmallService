using SmallService.Models;
using System.Collections.Generic;

namespace SmallService.Service
{
    public interface IRankService
    {
        public void Add(Customer customer);

        public IEnumerable<CustomerRank> Rank(int start, int end);

        public IEnumerable<CustomerRank> RankByCustomerId(long customerId, int high, int low);
    }
}
