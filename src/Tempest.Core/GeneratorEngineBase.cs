using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Configuration.Operations.Sourcing;
using Tempest.Core.Configuration.Operations.Transformation;
using Tempest.Core.Options;

namespace Tempest.Core
{

    /// <summary>
    /// Generator base. Has the task of configuring options and steps for scaffolder
    /// Also executes the scaffolder
    /// </summary>
    public abstract class GeneratorBase : ExecutableGeneratorBase
    {
        protected GeneratorBase(IScaffolderServiceFactory scaffolderServiceFactory) : base(scaffolderServiceFactory)
        {
        }

        
    }


    public abstract class GeneratorEngineBase
    {

        private readonly OptionExecutor _optionExecutor = new OptionExecutor();
        
        protected internal IList<OperationStep> Steps { get; set; } = new List<OperationStep>();

        protected internal IList<OperationTransformer> GlobalTransformers { get; set; } = new List<OperationTransformer>();

        protected abstract DirectoryInfo BuildTargetPath(GeneratorContext generatorContext);

        protected abstract DirectoryInfo BuildTemplatePath(GeneratorContext generatorContext);

        /// <summary>
        ///     Setup the options
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<ConfigurationOption> SetupOptions()
        {
            yield break;
        }

        /// <summary>
        ///     Execute your codez!
        /// </summary>
        protected abstract void ExecuteCore();

        protected virtual IEnumerable<OperationTransformer> GetEligibleTransformers(OperationStep step)
            => GlobalTransformers.Union(step.GetTransformers());

        public virtual void Run(GeneratorContext context)
        {
            var options = SetupOptions();
            _optionExecutor.Execute(options.ToArray(), context.Arguments);
            ExecuteCore();

            var sourcingContext = new SourcingContext
            {
                TemplateRoot = context.TemplateDirectory,
                TargetRoot = BuildTargetPath(context)
            };

            var operationBuilder = new OperationBuilder();
            var operations = operationBuilder.Build(Steps, GlobalTransformers, sourcingContext);

            foreach (var operation in operations)
            {
                if (context.ShouldLogOperation())
                {
                    // Log
                }
                operation.Execute();
            }
        }


    }
}