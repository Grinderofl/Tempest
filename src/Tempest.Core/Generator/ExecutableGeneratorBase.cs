using System.Collections.Generic;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Operations;
using Tempest.Core.Scaffolding;

namespace Tempest.Core.Generator
{
    public abstract class ExecutableGeneratorBase : IExecutableGenerator
    {

        protected abstract void ConfigureOptions(OptionsFactory options);

        IEnumerable<ConfigurationOption> IExecutableGenerator.CreateOptions()
        {
            return CreateOptionsCore();
        }

        protected virtual IEnumerable<ConfigurationOption> CreateOptionsCore()
        {
            var factory = new OptionsFactory();
            ConfigureOptions(factory);
            return factory.Options;
        }

        ScaffoldOperationConfiguration IExecutableGenerator.ConfigureOperations(ScaffoldOperationConfiguration configuration)
        {
            var configurer = new ScaffolderConfigurer();
            ConfigureGenerator(configurer);
            ((IScaffoldConfigurer)configurer).ConfigureOperations(configuration);
            return configuration;
        }

        protected abstract void ConfigureGenerator(IScaffold scaffold);
    }
}