using System.Threading.Tasks;
using Domain.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task ForceBeginTransaction();

        Task CommitTransaction();

        Task RollbackTransaction();

        Task<int> SaveChangesAsync();


        IGenericRepository<Customer> CustomerRepository { get; }

        IGenericRepository<User> IdentityRepository { get; }

    }
}