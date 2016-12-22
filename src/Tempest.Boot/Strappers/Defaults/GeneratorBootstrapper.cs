using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions;

namespace Tempest.Boot.Strappers.Defaults
{
    public class GeneratorBootstrapper : ConventionBootstrapper
    {
        public GeneratorBootstrapper() : base(new DefaultServiceProviderFactory())
        {
        }


        protected override void ConfigureBootstrapper()
        {
        }
    }
}