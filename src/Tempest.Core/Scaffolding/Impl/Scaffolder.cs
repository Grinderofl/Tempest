using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tempest.Core.Configuration.Operations.Sourcing;
using Tempest.Core.Operations;
using Tempest.Core.Operations.Execution;
using Tempest.Core.Options;

namespace Tempest.Core.Scaffolding.Impl
{
    // Will be: Scaffolder : IScaffolder
    public class Scaffolder : IScaffolder
    {
        private readonly GeneratorContext _generatorContext;
        private readonly ScaffoldOperationConfiguration _configuration;
        private readonly IEnumerable<IScaffoldConfigurer> _configurers;
        private readonly IExecutableGenerator _generator;
        private readonly OperationBuilder _operationBuilder;
        private readonly OptionExecutor _optionExecutor;
        private readonly IOperationExecutor _operationExecutor;
        public Scaffolder(GeneratorContext generatorContext, ScaffoldOperationConfiguration configuration, IEnumerable<IScaffoldConfigurer> configurers, IExecutableGenerator generator, OperationBuilder operationBuilder, OptionExecutor optionExecutor, IOperationExecutor operationExecutor)
        {
            _generatorContext = generatorContext;
            _configuration = configuration;
            _configurers = configurers;
            _generator = generator;
            _operationBuilder = operationBuilder;
            _optionExecutor = optionExecutor;
            _operationExecutor = operationExecutor;
        }

        protected DirectoryInfo BuildTargetPath(GeneratorContext generatorContext,
            ScaffoldOperationConfiguration configuration)
            =>
            new DirectoryInfo(Path.Combine(generatorContext.WorkingDirectory.FullName, configuration.TargetSubDirectory));

        public void Scaffold()
        {
            var options = _generator.CreateOptions();
            _optionExecutor.Execute(options.ToArray(), _generatorContext.Arguments);
            
            var configuration = _generator.ConfigureOperations(_configuration);

            foreach (var configurer in _configurers.OrderBy(x => x.Order))
                configurer.ConfigureOperations(configuration);

            var sourcingContext = new SourcingContext()
            {
                TargetRoot = BuildTargetPath(_generatorContext, configuration),
                TemplateRoot = _generatorContext.TemplateDirectory
            };

            var operations = _operationBuilder.Build(configuration.Steps, configuration.GlobalTransformers,
                sourcingContext);

            // Maybe ask user if this is OK?
            _operationExecutor.Execute(operations);


        }
    }
}