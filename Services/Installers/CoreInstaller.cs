using Common.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Customer;

namespace Services.Installers
{
    public class CoreInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
