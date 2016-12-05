using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Tempest.Boot
{
    public abstract class ConventionTempestBootstrapper : TempestBootstrapper<IServiceCollection>
    {
        private readonly Queue<IServiceConfigurationConvention> _conventions =
            new Queue<IServiceConfigurationConvention>();

        protected ConventionTempestBootstrapper(IServiceProviderFactory<IServiceCollection> serviceProviderFactory)
            : base(serviceProviderFactory)
        {
        }

        protected virtual void AddConvention<T>() where T : IServiceConfigurationConvention, new()
        => AddConvention(new T());

        protected virtual void AddConvention(IServiceConfigurationConvention convention)
            => _conventions.Enqueue(convention);

        protected override void ConfigureLocalServices(IServiceCollection services)
        {
            ConfigureConventions();
            foreach (var convention in _conventions)
                convention.Configure(services);
        }

        protected abstract void ConfigureConventions();
    }
}