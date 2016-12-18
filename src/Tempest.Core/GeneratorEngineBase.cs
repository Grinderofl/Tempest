using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Tempest.Core.Options;
using Tempest.Core.Setup.OperationBuilding;
using Tempest.Core.Setup.Sourcing;
using Tempest.Core.Setup.Transformation;

namespace Tempest.Core
{
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
                if(context.ShouldLogOperation())
                operation.Execute();
            }
        }


    }
}