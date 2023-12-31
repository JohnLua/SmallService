using SmallService.Models;

namespace SmallService.Service
{
    public interface ICustomerScoreService
    {
        public bool ContainsCustomer(long customerId);

        public void Add(Customer customer);

        public void Update(long customerId, decimal score);

        public Customer Get(long customerId);

    }
}
