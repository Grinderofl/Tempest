using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
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
        /// Setup the options
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<ConfigurationOption> SetupOptions()
        {
            yield break;
        }

        /// <summary>
        /// Execute your codez!
        /// </summary>
        protected abstract void ExecuteCore();
        
        public virtual void Run(GeneratorContext context)
        {
            var options = SetupOptions();
            _optionExecutor.Execute(options.ToArray(), context.Arguments);
            ExecuteCore();

            var sourcingContext = new SourcingContext()
            {
                TemplateRoot = BuildTemplatePath(context),
                TargetRoot = BuildTargetPath(context)
            };

            foreach (var step in Steps)
            {
                var source = step.GetSource();
                var sourceResult = source.Generate(sourcingContext);

                var transformerContext = new TransformerContext()
                {
                    TransformationStream = sourceResult.OutputStream
                };
                foreach (var globalTransformer in GlobalTransformers)
                {
                    var globalTransformationResult = globalTransformer.Transform(transformerContext);
                    transformerContext.TransformationStream = globalTransformationResult.OutputStream;
                }

                foreach (var transformer in step.GetTransformers())
                {
                    var transformResult = transformer.Transform(transformerContext);
                    transformerContext.TransformationStream = transformResult.OutputStream;
                }

                var emissionContext = new EmissionContext()
                {
                    EmissionStream = transformerContext.TransformationStream,
                    TargetDirectory = sourcingContext.TargetRoot
                };

                foreach (var emitter in step.GetEmitters())
                {
                    emitter.Emit(emissionContext);
                }
            }


        }
    }

}



    