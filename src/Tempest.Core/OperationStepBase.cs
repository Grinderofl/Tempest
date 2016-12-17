using System.Collections.Generic;
using Tempest.Core.Setup.Persisters;
using Tempest.Core.Setup.Sourcing;
using Tempest.Core.Setup.Transformation;

namespace Tempest.Core
{
    public abstract class OperationStepBase
    {
        private readonly SourceFactory _sourceFactory;
        protected IList<PersisterFactory> InternalEmitters = new List<PersisterFactory>();
        protected IList<OperationTransformer> InternalTransformers = new List<OperationTransformer>();

        protected OperationStepBase(SourceFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }

        public SourceFactory GetSource() => _sourceFactory;

        public IEnumerable<OperationTransformer> GetTransformers() => InternalTransformers;

        public IEnumerable<PersisterFactory> GetEmitters() => InternalEmitters;
    }
}