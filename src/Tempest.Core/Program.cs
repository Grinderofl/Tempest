using System.Collections.Generic;
using Tempest.Core.Emission;
using Tempest.Core.Sourcing;
using Tempest.Core.Transformation;

namespace Tempest.Core
{
    public class EngineBase
    {
        // Engine has steps it needs to take for each ... file? Item? what now?
        // So the steps do the following - Generate, Transform, and Emit.
        // Transform is an optional step. TransformationContext is just changed into EmissionContext

        public IList<TemplateStep> Steps { get; set; } = new List<TemplateStep>();
        
        

    }

    public class Engine : EngineBase
    {
        public CreateFactory Create { get; set; } = new CreateFactory();
        public UpdateFactory Update { get; set; } = new UpdateFactory();
        public CopyFactory Copy { get; set; } = new CopyFactory();
    }

    /// <summary>
    /// Contains methods that should copy a template file
    /// </summary>
    public class CopyFactory
    {
        
    }

    /// <summary>
    /// Contains methods that create stuff out of thin air.
    /// </summary>
    public class CreateFactory
    {
        
        public TemplateStep FromString(string source)
        {
            return new TemplateStep(Generators.FromString(source));
        }
    }

    public class UpdateFactory
    {
        // Methods that read an existing file in output directory
    }

    public class TemplateStep
    {
        public TemplateStep(Source source)
        {
            Source = source;
        }

        public Source Source { get; }
        public IEnumerable<Transformer> Transformers { get; set; }
        public IEnumerable<Emitter> Emitters { get; set; }
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


    