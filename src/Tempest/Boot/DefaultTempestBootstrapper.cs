using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions;

namespace Tempest.Boot
{
    public class DefaultTempestBootstrapper : ConventionTempestBootstrapper
    {
        private DefaultTempestBootstrapper(IServiceProviderFactory<IServiceCollection> serviceProviderFactory)
            : base(serviceProviderFactory)
        {
        }

        public static TempestBootstrapper<IServiceCollection> Create()
        {
            return new DefaultTempestBootstrapper(new DefaultServiceProviderFactory());
        }

        protected override void ConfigureConventions()
        {
            AddConvention<RegisterDefaultImplementations>();
        }
    }
}