using System.Collections.Generic;
using Tempest.Core.Setup.Persisters;
using Tempest.Core.Setup.Sourcing;
using Tempest.Core.Setup.Transformation;

namespace Tempest.Core
{
    public abstract class OperationStepBase
    {
        private readonly SourceGenerator _sourceGenerator;
        protected IList<PersisterFactory> InternalEmitters = new List<PersisterFactory>();
        protected IList<OperationTransformer> InternalTransformers = new List<OperationTransformer>();

        protected OperationStepBase(SourceGenerator sourceGenerator)
        {
            _sourceGenerator = sourceGenerator;
        }

        public SourceGenerator GetSource() => _sourceGenerator;

        public IEnumerable<OperationTransformer> GetTransformers() => InternalTransformers;

        public IEnumerable<PersisterFactory> GetEmitters() => InternalEmitters;
    }
}