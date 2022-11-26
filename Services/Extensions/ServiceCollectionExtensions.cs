﻿using System;
using System.Linq;
using System.Reflection;
using Common.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void RegisterServicesInAssembly(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var installers = Assembly.GetExecutingAssembly()
                .ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            installers.ForEach(x => x.InstallServices(services, configuration));
        }
    }
}
