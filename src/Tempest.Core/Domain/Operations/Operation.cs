using System;
using System.IO;
using Tempest.Core.Domain.Streaming;
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
        private readonly IStreamFactory _sourceStreamFactory;
        private readonly Func<Stream, Stream> _transformer;
        private readonly IStreamEmitter _emitter;

        public Operation(IStreamFactory sourceStreamFactory, Func<Stream, Stream> transformer, IStreamEmitter emitter)
        {
            _sourceStreamFactory = sourceStreamFactory;
            _transformer = transformer;
            _emitter = emitter;
        }

        public virtual void Execute()
        {
            var stream = _sourceStreamFactory.Create();
            var transformedStream = _transformer(stream);
            _emitter.Emit(transformedStream);
        }
    }
}
