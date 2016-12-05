using System;
using Microsoft.Extensions.DependencyInjection;

namespace Tempest.Boot
{

    /// <summary>
    /// Simple bootstrapper base for console applications
    /// </summary>
    public abstract class TempestBootstrapper
    {
        protected abstract IServiceProvider CreateProvider();

        public virtual int Execute(string[] args)
        {
            var provider = CreateProvider();
            var executor = provider.GetService<ICommandLineExecutor>();
            return executor.Execute(args);
        }
    }

    /// <summary>
    /// Bootstrapper with specific type of Service Builder Container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TempestBootstrapper<T> : TempestBootstrapper
    {
        private readonly IServiceProviderFactory<T> _serviceProviderFactory;

        public static TempestBootstrapper<TBuilder> Create<TBuilder>(IServiceProviderFactory<TBuilder> factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return new TempestBootstrapper<TBuilder>(factory);
        }

        protected TempestBootstrapper(IServiceProviderFactory<T> serviceProviderFactory)
        {
            if (serviceProviderFactory == null) throw new ArgumentNullException(nameof(serviceProviderFactory));
            _serviceProviderFactory = serviceProviderFactory;
        }

        protected override IServiceProvider CreateProvider()
        {
            var services = new ServiceCollection();
            ConfigureLocalServices(services);
            var builder = _serviceProviderFactory.CreateBuilder(services);
            return _serviceProviderFactory.CreateServiceProvider(builder);
        }

        protected virtual void ConfigureLocalServices(IServiceCollection services)
        {
        }
    }
}