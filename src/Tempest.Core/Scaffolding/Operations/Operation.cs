using System;
using Tempest.Core.Scaffolding.Persistence;
using Tempest.Core.Scaffolding.Providers;
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
        private readonly IStreamProvider _streamProvider;
        private readonly IStreamTransformer _transformer;
        private readonly IStreamPersister _persister;

        
        public Operation(IStreamProvider streamProvider, IStreamTransformer transformer, IStreamPersister persister)
        {
            if (streamProvider == null) throw new ArgumentNullException(nameof(streamProvider));
            if (transformer == null) throw new ArgumentNullException(nameof(transformer));
            if (persister == null) throw new ArgumentNullException(nameof(persister));
            _streamProvider = streamProvider;
            _transformer = transformer;
            _persister = persister;
        }

        public virtual void Execute()
        {
            var stream = _streamProvider.Provide();
            var transformedStream = _transformer.Transform(stream);
            _persister.Persist(transformedStream);
        }
    }
}
