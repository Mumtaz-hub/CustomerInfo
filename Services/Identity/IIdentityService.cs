using Domain.Entities;
using ViewModel;

namespace Services.Customer
{
    public interface IIdentityService
    {

        IdentityResponseViewModel Authenticate(IdentityRequestViewModel model);
        User GetById(long id);
    }
}