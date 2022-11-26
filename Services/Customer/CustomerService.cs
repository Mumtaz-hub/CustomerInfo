using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Common.Constants;
using Common.Extensions;
using Core.Interfaces;
using MapsterMapper;
using Microsoft.Extensions.Options;
using ViewModel;

namespace Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerApiSettings customerApiSettings;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<CustomerApiSettings> customerApiSettings, IHttpClientFactory httpClientFactory)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.customerApiSettings = customerApiSettings.Value;
            this.httpClientFactory = httpClientFactory;
        }

      
        public async Task<Result<bool>> SaveCustomer(List<CustomerViewModel> model)
        {
            var entityList = model.Select(mapper.Map<Domain.Entities.Customer>).ToList();
            await unitOfWork.CustomerRepository.AddRange(entityList);
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransaction();
            return new SuccessResult<bool>(true);
        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomerHistory()
        {
            var data = await unitOfWork.CustomerRepository.GetAll(orderBy: i => i.OrderByDescending(k => k.Id));
            return data.Select(mapper.Map<CustomerViewModel>).ToList();
        }
       
    }
}