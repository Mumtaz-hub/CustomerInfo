using System;
using Mapster;
using ViewModel;

namespace Services.MapsterRegistration
{
    public class CustomerMapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CustomerViewModel, Domain.Entities.Customer>()
                .Map(dest => dest.FirstName, opts => opts.FirstName)
                .Map(dest => dest.LastName, opts => opts.LastName)
                .Map(dest => dest.Age, opts => opts.Age)
                .Map(dest => dest.CreationTs, opts => DateTime.UtcNow);

            config.NewConfig<Domain.Entities.Customer, CustomerViewModel>()
                .Map(dest => dest.FirstName, opts => opts.FirstName)
                .Map(dest => dest.LastName, opts => opts.LastName)
                .Map(dest => dest.Age, opts => opts.Age);

        }
    }
}
