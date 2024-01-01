using SmallService.Models;

namespace SmallService.Service
{
    public interface ICustomerScoreService
    {
        public bool Contains(long customerId);

        public void Add(Customer customer);

        public Customer Update(long customerId, decimal score);

        public Customer Get(long customerId);

    }
}
