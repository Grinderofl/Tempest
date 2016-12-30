using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Strappers.Execution;

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

        public void Execute()
        {
            Execute(new GeneratorExecutor());
        }
    }
}