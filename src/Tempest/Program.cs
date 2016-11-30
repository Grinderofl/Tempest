using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }


    // 1) Generator generates (out of thin air or by copying or by downloading or whatever) a stream
    // 2) Transformer takes the stream and does whatever (like changing all occurrences of 'Microsoft' into 'YourMom') and passes that on as new stream
    // 3) Emitter takes the stream and emits it wherever (like into a file, or maybe into github repo, trololo)
    // 4) Profit, because no steps are "???" =D


    /// <summary>
    /// Generates an output stream
    /// Could be:
    /// GenerateEmpty
    /// GenerateByCopying
    /// GenerateFromWebstream
    /// </summary>
    public abstract class Generator
    {
        public virtual GenerationResult Generate(GenesisContext context)
        {
            return new GenerationResult() {OutputStream = GenerateCore(context)};
        }

        protected abstract Stream GenerateCore(GenesisContext context);
    }


    /// <summary>
    /// Transforms the input stream into output transformation result
    /// Could be:
    /// TokenTransformer
    /// XdtTransformer
    /// JsonTransformer
    /// 
    /// </summary>
    public abstract class Transformer
    {
        public virtual TransformationResult Transform(TransformerContext context)
        {
            return new TransformationResult()
            {
                OutputStream = TransformCore(context)
            };
        }

        protected virtual Stream TransformCore(TransformerContext context)
        {
            return context.InputStream;
        }
    }

    public class Emitter
    {
        public virtual EmissionResult Emit(EmissionContext context)
        {
            return new EmissionResult();
        }
    }

    public class GenerationResult
    {
        public Stream OutputStream { get; set; }
    }


    public class TransformationResult
    {
        public Stream OutputStream { get; set; }
    }

    public class EmissionResult
    {
        
    }

    public class TransformerContext
    {
        public Stream InputStream { get; set; }

        public string ReadInputAsString()
        {
            throw new NotImplementedException();
        }
    }

    public class GenesisContext
    {
        
    }

    public class EmissionContext
    {
        
    }
}

    // What does a template generator need?

    // Copy files from one location to another, possibly rename
    // Replace text (tokens) in files
    // Modify project structure (edit project files) - serialization?
    // Simple syntax


    
}
