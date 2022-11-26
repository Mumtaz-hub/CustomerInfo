using Ardalis.GuardClauses;
using Common;
using Common.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;


namespace Api.Installers
{
    public class CoreServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            Guard.Against.Null(services, nameof(services));

            services.AddHttpClient("Customer");

            services.AddCors();
            services.AddControllers();
            services.AddSingleton(configuration);
            services.AddLogging();
            
            AddSerializerSettings(services);
            AddAppSettings(services, configuration);
            AddOauthSettings(services, configuration);
        }

        private object GetRetryPolicy()
        {
            throw new NotImplementedException();
        }

        private static void AddSerializerSettings(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
            // .AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); });
        }

        private static void AddAppSettings(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection(AppSettings.Key));
        }

        private static void AddOauthSettings(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<OauthSettings>()
             .Bind(configuration.GetSection(OauthSettings.Key))
             .ValidateDataAnnotations();
        }
    }
}
