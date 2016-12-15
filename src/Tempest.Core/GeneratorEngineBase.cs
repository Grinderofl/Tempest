using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tempest.Core.Emission;
using Tempest.Core.Options;
using Tempest.Core.Setup;
using Tempest.Core.Sourcing;
using Tempest.Core.Transformation;

namespace Tempest.Core
{
    public abstract class GeneratorEngineBase
    {
        private readonly OptionExecutor _optionExecutor = new OptionExecutor();

        protected internal IList<ScaffoldStep> Steps { get; set; } = new List<ScaffoldStep>();

        protected internal IList<Transformer> GlobalTransformers { get; set; } = new List<Transformer>();

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

        protected virtual IEnumerable<Transformer> GetEligibleTransformers(ScaffoldStep step)
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
            var operations = operationBuilder.Build(Steps, sourcingContext);
            foreach (var operation in operations)
            {
                operation.Execute();
            }

            //var executors = new List<ScaffoldStepExecutor>(Steps.Count);
            //executors.AddRange(Steps.Select(scaffoldStep => new ScaffoldStepExecutor(scaffoldStep)));

            //RetrieveSources(sourcingContext, executors);
            //Transform(executors, GlobalTransformers);
            //Emit(sourcingContext, executors);
        }

        //protected virtual void Emit(SourcingContext sourcingContext, IList<ScaffoldStepExecutor> executors)
        //{
        //    foreach (var executor in executors)
        //        executor.ExecuteEmitters(sourcingContext);
        //}

        //protected virtual void Transform(IList<ScaffoldStepExecutor> executors, ICollection<Transformer> globalTransformers)
        //{
        //    foreach (var executor in executors)
        //        executor.ExecuteTransformers(globalTransformers);
        //}

        //protected virtual void RetrieveSources(SourcingContext sourcingContext, IList<ScaffoldStepExecutor> executors)
        //{
        //    foreach (var executor in executors)
        //        executor.ExecuteSources(sourcingContext);
        //}
    }
}