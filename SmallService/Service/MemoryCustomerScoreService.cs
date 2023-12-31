using SmallService.Models;
using System.Collections.Generic;

namespace SmallService.Service
{
    public class MemoryCustomerScoreService : ICustomerScoreService
    {
        private readonly Dictionary<long, Customer> _customerScoreCache = new Dictionary<long, Customer>();

        public bool ContainsCustomer(long customerId)
        {
            return _customerScoreCache.ContainsKey(customerId);
        }

        public void Add(Customer customer)
        {
            _customerScoreCache.Add(customer.CustomerId, customer);
        }

        public void Update(long customerId, decimal score)
        {
            _customerScoreCache[customerId].Score += score;
        }

        public Customer Get(long customerId)
        {
            return _customerScoreCache[customerId];
        }
    }
}
