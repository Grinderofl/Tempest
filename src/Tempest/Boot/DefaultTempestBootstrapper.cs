using Microsoft.Extensions.DependencyInjection;

namespace Tempest.Boot
{
    public class DefaultTempestBootstrapper : TempestBootstrapper<IServiceCollection>
    {
        private DefaultTempestBootstrapper(IServiceProviderFactory<IServiceCollection> serviceProviderFactory) : base(serviceProviderFactory)
        {
        }

        public static TempestBootstrapper<IServiceCollection> Create()
        {
            return new DefaultTempestBootstrapper(new DefaultServiceProviderFactory());
        }
    }
}