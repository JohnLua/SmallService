using SmallService.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmallService.Service
{
    public class MemoryLeaderBoardService : ILeaderBoardService
    {
        private readonly object _lockObject = new();
        private readonly ICustomerScoreService _customerScoreService;
        private readonly IRankService _rankService;

        public MemoryLeaderBoardService(ICustomerScoreService customerScoreService, IRankService rankService)
        {
            _customerScoreService = customerScoreService;
            _rankService = rankService;
        }

        public decimal Update(long customerId, decimal score)
        {
            lock (_lockObject)
            {
                if (_customerScoreService.Contains(customerId))
                {
                    var updateCustomer = _customerScoreService.Update(customerId, score);
                    _rankService.Update(updateCustomer);

                    return updateCustomer.Score;
                }

                var newCustomer = new Customer
                {
                    CustomerId = customerId,
                    Score = score
                };

                _rankService.Add(newCustomer);
                _customerScoreService.Add(newCustomer);

                return newCustomer.Score;
            }
        }

        public IEnumerable<CustomerRank> Rank(int start, int end)
        {
            lock (_lockObject)
            {
                return _rankService.Rank(start, end);
            }
        }

        public IEnumerable<CustomerRank> RankByCustomerId(long customerId, int high, int low)
        {
            if (!_customerScoreService.Contains(customerId))
            {
                return Enumerable.Empty<CustomerRank>();
            }

            lock (_lockObject)
            {
                return _rankService.RankByCustomerId(customerId, high, low);
            }
        }
    }
}
