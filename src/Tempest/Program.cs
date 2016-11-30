using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Emission;
using Tempest.Generation;
using Tempest.Transformation;

namespace Tempest
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }


    public class Engine
    {
        // Engine has steps it needs to take for each ... file? Item? what now?
        // So the steps do the following - Generate, Transform, and Emit.
        // Transform is an optional step. TransformationContext is just changed into EmissionContext

        public IList<TemplateStep> Steps { get; set; } = new List<TemplateStep>();
        


    }

    public class TemplateStep
    {
        public Generator Generator { get; set; }
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


    