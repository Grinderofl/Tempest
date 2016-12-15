using System;
using System.IO;
using Tempest.Core.Emission;

namespace Tempest.Core.Domain.Operations
{
    /// <summary>
    /// Represents a single unit of work.
    /// In the context of scaffolder, a unit of work is:
    /// 
    /// 1) The action of taking a source stream
    /// 2) Utilising various transformers to transform its contents
    /// 3) The action of outputting to a destination stream
    /// </summary>
    public class Operation
    {
        private readonly Stream _sourceStream;
        private readonly Func<Stream, Stream> _transformer;
        private readonly ActualEmitter _emitter;

        public Operation(Stream sourceStream, Func<Stream, Stream> transformer, ActualEmitter emitter)
        {
            _sourceStream = sourceStream;
            _transformer = transformer;
            _emitter = emitter;
        }

        public virtual void Execute()
        {
            _emitter.Emit(_transformer(_sourceStream));
        }
    }
}
