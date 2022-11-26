using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Services.Customer;
using ViewModel;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        [Route("api/customerhistory")]
        public async Task<IEnumerable<CustomerViewModel>> GetCustomerHistory()
        {
            return await customerService.GetCustomerHistory();
        }
    }
} 