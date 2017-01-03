using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.Options.Impl;
using Tempest.Core.Options.Rendering;

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

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<RenderOptions>();
            base.ConfigureServices(services);
        }

        public void Execute()
        {
            Execute(new GeneratorExecutor());
        }
    }
}