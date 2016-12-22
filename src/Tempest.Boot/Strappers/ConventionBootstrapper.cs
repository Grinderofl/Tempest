using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tempest.Boot.Conventions;
using Tempest.Boot.Conventions.Defaults;

namespace Tempest.Boot.Strappers
{
    public abstract class ConventionBootstrapper : BootstrapperBase<IServiceCollection>
    {
        private readonly Queue<IServiceConfigurationConvention> _conventions =
            new Queue<IServiceConfigurationConvention>();

        protected ConventionBootstrapper(IServiceProviderFactory<IServiceCollection> serviceProviderFactory)
            : base(serviceProviderFactory)
        {
        }

        public virtual void RegisterConvention(IServiceConfigurationConvention convention)
        {
            AddConvention(convention);
        }
        protected virtual void AddConvention<T>() where T : IServiceConfigurationConvention, new()
            => AddConvention(new T());

        protected virtual void AddConvention(IServiceConfigurationConvention convention)
            => _conventions.Enqueue(convention);

        protected override void ConfigureServices(IServiceCollection services)
        {
            ConfigureDefaultServices(services);
            ConfigureBootstrapper();
            foreach (var convention in _conventions)
                convention.Configure(services);
        }

        protected virtual void ConfigureDefaultServices(IServiceCollection services)
        {
            AddConvention<TempestConfigurationRegistrationConvention>();
            services.AddLogging();
        }

        protected override IServiceProvider CreateProvider()
        {
            var provider = base.CreateProvider();
            ConfigureProvider(provider);
            return provider;
        }

        protected virtual void ConfigureProvider(IServiceProvider provider)
        {
            var loggerFatory = provider.GetRequiredService<ILoggerFactory>();
            var configuration = provider.GetRequiredService<IConfigurationRoot>();
            ConfigureLogging(loggerFatory, configuration);
        }

        protected virtual void ConfigureLogging(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            loggerFactory.AddConsole(configuration.GetSection("Logging")).AddDebug();
        }

        protected abstract void ConfigureBootstrapper();
    }
}