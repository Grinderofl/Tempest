using System;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Strappers.Execution;

namespace Tempest.Boot.Strappers
{
    /// <summary>
    ///     Simple bootstrapper base for console applications
    /// </summary>
    public abstract class BootstrapperBase
    {
        protected abstract IServiceProvider CreateProvider();

        public virtual int Execute(IBootstrapperExecutor executor)
        {
            var provider = CreateProvider();
            return executor.Execute(provider);
        }
    }

    

    /// <summary>
    ///     Bootstrapper with specific type of Service Builder Container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BootstrapperBase<T> : BootstrapperBase
    {
        private readonly IServiceProviderFactory<T> _serviceProviderFactory;

        protected BootstrapperBase(IServiceProviderFactory<T> serviceProviderFactory)
        {
            if (serviceProviderFactory == null) throw new ArgumentNullException(nameof(serviceProviderFactory));
            _serviceProviderFactory = serviceProviderFactory;
        }

        protected override IServiceProvider CreateProvider()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var builder = _serviceProviderFactory.CreateBuilder(services);
            return _serviceProviderFactory.CreateServiceProvider(builder);
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }
    }
}