using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions.Defaults;
using Tempest.Boot.Strappers.Execution;

namespace Tempest.Boot.Strappers.Defaults
{
    public class TempestBootstrapper : ConventionBootstrapper
    {
        private TempestBootstrapper(IServiceProviderFactory<IServiceCollection> serviceProviderFactory)
            : base(serviceProviderFactory)
        {
        }

        public static TempestBootstrapper Create()
        {
            return new TempestBootstrapper(new DefaultServiceProviderFactory());
        }

        protected override void ConfigureBootstrapper()
        {
            AddConvention<TempestAssemblyImplementationRegistrationConvention>();
        }

        public void Execute(string[] args)
        {
            Execute(new TempestExecutor(args));
        }
    }
}