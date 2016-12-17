using System;
using Tempest.Core.Scaffolding.Persistence;
using Tempest.Core.Scaffolding.Sources;
using Tempest.Core.Scaffolding.Transforms;

namespace Tempest.Core.Scaffolding.Operations
{
    /// <summary>
    /// Represents a single unit of work.
    /// In the context of scaffolder, a unit of work is:
    /// 
    /// 1) The action of taking a source stream
    /// 2) Utilising various transformers to transform its contents
    /// 3) The action of persisting transformed stream
    /// </summary>
    public class Operation
    {
        private readonly IStreamSource _sourceStreamSource;
        private readonly IStreamTransformer _transformer;
        private readonly IStreamPersister _persister;

        
        public Operation(IStreamSource sourceStreamSource, IStreamTransformer transformer, IStreamPersister persister)
        {
            if (sourceStreamSource == null) throw new ArgumentNullException(nameof(sourceStreamSource));
            if (transformer == null) throw new ArgumentNullException(nameof(transformer));
            if (persister == null) throw new ArgumentNullException(nameof(persister));
            _sourceStreamSource = sourceStreamSource;
            _transformer = transformer;
            _persister = persister;
        }

        public virtual void Execute()
        {
            var stream = _sourceStreamSource.Create();
            var transformedStream = _transformer.Transform(stream);
            _persister.Persist(transformedStream);
        }
    }
}
