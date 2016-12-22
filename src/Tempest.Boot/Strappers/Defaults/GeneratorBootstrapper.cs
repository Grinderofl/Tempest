using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions;

namespace Tempest.Boot.Strappers.Defaults
{
    public class GeneratorBootstrapper : ConventionBootstrapper
    {
        public GeneratorBootstrapper() : base(new DefaultServiceProviderFactory())
        {
        }

        public virtual void RegisterConvention(IServiceConfigurationConvention convention)
        {
            AddConvention(convention);
        }

        protected override void ConfigureBootstrapper()
        {
        }
    }
}