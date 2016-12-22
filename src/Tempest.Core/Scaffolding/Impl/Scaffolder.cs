using System.IO;
using System.Linq;
using Tempest.Core.Configuration.Operations.Sourcing;
using Tempest.Core.Configuration.Scaffolding;
using Tempest.Core.Operations;
using Tempest.Core.Operations.Execution;
using Tempest.Core.Operations.Execution.Impl;
using Tempest.Core.Options.Impl;

namespace Tempest.Core.Scaffolding.Impl
{

    public class Scaffolder : IScaffolder
    {
        private readonly GeneratorContext _generatorContext;
        private readonly IOperationBuilder _operationBuilder;
        private readonly IOperationExecutor _operationExecutor;
        private readonly IConfigurationResolver _configurationResolver;

        public Scaffolder(GeneratorContext generatorContext,
            IOperationBuilder operationBuilder, IOperationExecutor operationExecutor, IConfigurationResolver configurationResolver)
        {
            _generatorContext = generatorContext;
            _operationBuilder = operationBuilder;
            _operationExecutor = operationExecutor;
            _configurationResolver = configurationResolver;
        }

        protected DirectoryInfo BuildTargetPath(GeneratorContext generatorContext,
            ScaffoldOperationConfiguration configuration)
            =>
            new DirectoryInfo(Path.Combine(generatorContext.WorkingDirectory.FullName, configuration.TargetSubDirectory));

        public void Scaffold()
        {
            var configuration = _configurationResolver.Resolve();

            var sourcingContext = new SourcingContext()
            {
                TargetRoot = BuildTargetPath(_generatorContext, configuration),
                TemplateRoot = _generatorContext.TemplateDirectory
            };

            var operations = _operationBuilder.Build(configuration, sourcingContext);

            // Maybe ask user if this is OK?
            _operationExecutor.Execute(operations);


        }
    }


}