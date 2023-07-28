using System;

namespace SmallService.Service
{
    public interface ICustomerService
    {
        void Update(Int64 customerId, decimal score);
    }
}
