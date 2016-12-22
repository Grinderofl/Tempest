using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions;

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
            ConfigureBootstrapper();
            foreach (var convention in _conventions)
                convention.Configure(services);
        }

        protected abstract void ConfigureBootstrapper();
    }
}