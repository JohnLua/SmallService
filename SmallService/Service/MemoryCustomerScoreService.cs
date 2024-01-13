using SmallService.Models;
using System.Collections.Generic;

namespace SmallService.Service
{
    public class MemoryCustomerScoreService : ICustomerScoreService
    {
        private readonly Dictionary<long, Customer> _customerScoreCache = new Dictionary<long, Customer>();

        public bool Contains(long customerId)
        {
            return _customerScoreCache.ContainsKey(customerId);
        }

        public void Add(Customer customer)
        {
            _customerScoreCache.Add(customer.CustomerId, customer);
        }

        public Customer Update(long customerId, decimal score)
        {
            var currentCustomer = _customerScoreCache[customerId];
            currentCustomer.Score += score;

            return currentCustomer;
        }

        public Customer Get(long customerId)
        {
            return _customerScoreCache[customerId];
        }
    }
}
