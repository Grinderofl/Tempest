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

        protected internal IList<TemplateStep> Steps { get; set; } = new List<TemplateStep>();

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

        protected virtual IEnumerable<Transformer> GetEligibleTransformers(TemplateStep step)
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


            // Perhaps step should execute itself? Or maybe stepexecutor instead
            foreach (var step in Steps)
            {
                var source = step.GetSource();
                var sourceResult = source.Generate(sourcingContext);

                foreach (var result in sourceResult)
                {
                    var transformerContext = new TransformerContext
                    {
                        TransformationStream = result.OutputStream,
                        Filename = result.FileName
                    };

                    var transformResult = new TransformationResult
                    {
                        OutputStream = transformerContext.TransformationStream,
                        Filename = transformerContext.Filename
                    };

                    foreach (var transformer in GetEligibleTransformers(step))
                    {
                        transformResult = transformer.Transform(transformerContext);
                        transformerContext.TransformationStream = transformResult.OutputStream;
                        transformerContext.Filename = transformResult.Filename;
                    }

                    var emissionContext = new EmissionContext
                    {
                        Filename = transformResult.Filename,
                        EmissionStream = transformerContext.TransformationStream,
                        TargetDirectory = sourcingContext.TargetRoot,
                        FilePath = result.FilePath
                    };

                    foreach (var emitter in step.GetEmitters())
                        emitter.Emit(emissionContext);
                }
            }
        }
    }
}