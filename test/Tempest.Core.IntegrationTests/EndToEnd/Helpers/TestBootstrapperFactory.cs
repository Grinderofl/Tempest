using System;
using Tempest.Boot.Runner.Activation.Impl;
using Tempest.Boot.Strappers.Defaults;
using Tempest.Core.Generator;

namespace Tempest.Core.IntegrationTests.EndToEnd.Helpers
{
    public class TestBootstrapperFactory : GeneratorBootstrapperFactory
    {
        private readonly Action<GeneratorBootstrapper> _configurationAction;

        public TestBootstrapperFactory( Action<GeneratorBootstrapper> configurationAction)
        {
            _configurationAction = configurationAction;
        }

        protected override void ConfigureBootstrapper(GeneratorBootstrapper bootstrapper,
            GeneratorContext generatorContext)
        {
            base.ConfigureBootstrapper(bootstrapper, generatorContext);
            _configurationAction(bootstrapper);
        }
    }
}