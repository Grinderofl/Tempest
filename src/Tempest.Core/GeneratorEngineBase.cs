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

        public IList<TemplateStep> Steps { get; set; } = new List<TemplateStep>();
        
        public IList<Transformer> GlobalTransformers { get; set; } = new List<Transformer>();

        protected abstract DirectoryInfo BuildTargetPath(RunnerContext runnerContext);

        protected abstract DirectoryInfo BuildTemplatePath(RunnerContext runnerContext);

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
        
        public virtual void Run(RunnerContext context)
        {
            // Parse arguments and execute options conventionally based on the ones that already exist
            if (context.Arguments != null)
            {
                
            }

            var options = SetupOptions();
            _optionExecutor.Execute(options, context.Arguments);
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



    