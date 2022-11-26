using Core.Interfaces;
using Core.Persistence;
using Data;
using Domain.Entities;

namespace Core.Repository
{
    public class IdentityRepository : GenericRepository<User>, IIdentityRepository
    {
        public IdentityRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
