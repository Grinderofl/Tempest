using System.Collections.Generic;
using Tempest.Core.Operations;
using Tempest.Core.Options;
using Tempest.Core.Setup.OperationBuilding;

namespace Tempest.Core
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
            ConfigureGenerator(configuration);
            return configuration;
        }

        protected abstract void ConfigureGenerator(ScaffoldOperationConfiguration configuration);
    }
}