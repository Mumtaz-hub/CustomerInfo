using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using ViewModel;

namespace Services.Customer
{
    public interface ICustomerService
    {
        Task<Result<bool>> SaveCustomer(List<CustomerViewModel> model);
        Task<IEnumerable<CustomerViewModel>> GetCustomerHistory();
    }
}