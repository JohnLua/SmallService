using SmallService.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmallService.Service
{
    public class MemoryRankService : IRankService
    {
        private readonly SortedList<Customer, Customer> _rankList = new SortedList<Customer, Customer>(new CustomerScoreComparer());
        private readonly ICustomerScoreService _customerScoreService;

        public MemoryRankService(ICustomerScoreService customerScoreService)
        {
            _customerScoreService = customerScoreService;
        }

        public void Add(Customer customer)
        {
            _rankList.Add(customer, customer);
        }

        public void Update(Customer customer)
        {
            _rankList.Remove(customer);
            _rankList.Add(customer, customer);
        }

        public IEnumerable<CustomerRank> Rank(int start, int end)
        {
            var rankStartIndex = start - 1;
            if (rankStartIndex < 0)
            {
                rankStartIndex = 0;
            }

            var rankEndIndex = end - 1;
            if (rankEndIndex >= _rankList.Count)
            {
                rankEndIndex = _rankList.Count - 1;
            }

            if (rankStartIndex > rankEndIndex)
            {
                return Enumerable.Empty<CustomerRank>();
            }

            var rank = 1;
            var results = new List<CustomerRank>();
            for (int i = rankStartIndex; i <= rankEndIndex && i < _rankList.Count; i++)
            {
                if (_rankList.Values[i].Score > 0)
                {
                    results.Add(new CustomerRank
                    {
                        CustomerId = _rankList.Values[i].CustomerId,
                        Score = _rankList.Values[i].Score,
                        Rank = rank++
                    }); ;
                }
            }

            return results;
        }

        public IEnumerable<CustomerRank> RankByCustomerId(long customerId, int high, int low)
        {
            var customer = _customerScoreService.Get(customerId);
            if (customer.Score <= 0)
            {
                return Enumerable.Empty<CustomerRank>();
            }

            var customerRank = _rankList.IndexOfKey(customer);
            var rankStartIndex = customerRank - high;
            if (rankStartIndex < 0)
            {
                rankStartIndex = 0;
            }

            var rankEndIndex = customerRank + low + 1;
            if (rankEndIndex >= _rankList.Count)
            {
                rankEndIndex = _rankList.Count;
            }

            var rank = 1;
            var results = new List<CustomerRank>();
            for (int i = rankStartIndex; i < rankEndIndex; i++)
            {
                if (_rankList.Values[i].Score > 0)
                {
                    results.Add(new CustomerRank
                    {
                        CustomerId = _rankList.Values[i].CustomerId,
                        Score = _rankList.Values[i].Score,
                        Rank = rank++
                    }); ;
                }
            }

            return results;
        }
    }
}
