using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Tempest.Core.Configuration;
using Tempest.Core.Sourcing;
using Tempest.Core.Transformation;

namespace Tempest.Core
{
    public abstract class GeneratorEngineBase
    {
        // Engine has steps it needs to take for each ... file? Item? what now?
        // So the steps do the following - Generate, Transform, and Emit.
        // Transform is an optional step. TransformationContext is just changed into EmissionContext

        private OptionExecutor _optionExecutor = new OptionExecutor();

        public IList<TemplateStep> Steps { get; set; } = new List<TemplateStep>();
        
        public IList<Transformer> GlobalTransformers { get; set; } = new List<Transformer>();

        /// <summary>
        /// Setup the options
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<OptionItem> SetupOptions();

        /// <summary>
        /// Execute your codez!
        /// </summary>
        protected abstract void ExecuteCore();
        
        public virtual void Run(RunnerContext context)
        {
            // This is where the pipeline should fill Steps and GlobalGenerators
            // It'll also set up the target directory depending on whatever variables, yes? There should be a function to set target directory.

            var options = SetupOptions();

            List<string> results = new List<string>();
            foreach (var item in options)
            {
                if(item.ShouldRender(results))
                    results.Add(item.Type.Renderer.Render(item));
            }

            // Now run steps

            var sourcingContext = new SourcingContext()
            {
                TemplateRoot = context.TempestDirectory + "/templates",
                TargetRoot = context.WorkingDirectory + projectnamefromsomewhere
            };

            foreach (var step in Steps)
            {
                var source = step.GetSource();
                source.Generate()
            }

        }
    }


    // 1) Generator generates (out of thin air or by copying or by downloading or whatever) a stream
    // 2) Transformer takes the stream and does whatever (like changing all occurrences of 'Microsoft' into 'YourMom') and passes that on as new stream
    // 3) Emitter takes the stream and emits it wherever (like into a file, or maybe into github repo, trololo)
    // 4) Profit, because no steps are "???" =D
}

    // What does a template generator need?

    // Copy files from one location to another, possibly rename
    // Replace text (tokens) in files
    // Modify project structure (edit project files) - serialization?
    // Simple syntax


    