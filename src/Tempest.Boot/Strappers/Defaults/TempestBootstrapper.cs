using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions.Defaults;

namespace Tempest.Boot.Strappers.Defaults
{
    public class TempestBootstrapper : ConventionBootstrapper
    {
        private TempestBootstrapper(IServiceProviderFactory<IServiceCollection> serviceProviderFactory)
            : base(serviceProviderFactory)
        {
        }

        public static BootstrapperBase<IServiceCollection> Create()
        {
            return new TempestBootstrapper(new DefaultServiceProviderFactory());
        }

        protected override void ConfigureBootstrapper()
        {
            AddConvention<TempestAssemblyImplementationRegistrationConvention>();
        }
    }
}