using Core.Interfaces;
using Core.Persistence;
using Data;
using Domain.Entities;

namespace Core.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
