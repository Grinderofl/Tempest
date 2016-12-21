using System;
using System.Collections.Generic;
using Tempest.Core.Configuration.Operations.Persistence;
using Tempest.Core.Configuration.Operations.Sourcing;
using Tempest.Core.Configuration.Operations.Transformation;
using Tempest.Core.Operations;

namespace Tempest.Core
{
    public abstract class OperationStepBase
    {
        private readonly Func<ScaffoldOperationConfiguration, SourceFactory> _sourceFactoryFunc;
        private readonly SourceFactory _sourceFactory;
        protected IList<PersisterFactory> InternalEmitters = new List<PersisterFactory>();
        protected IList<OperationTransformer> InternalTransformers = new List<OperationTransformer>();

        protected OperationStepBase(SourceFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }

        protected OperationStepBase(Func<ScaffoldOperationConfiguration, SourceFactory> sourceFactoryFunc)
        {
            _sourceFactoryFunc = sourceFactoryFunc;
        }

        public SourceFactory GetSource(ScaffoldOperationConfiguration configuration) => _sourceFactoryFunc?.Invoke(configuration) ?? _sourceFactory;

        public IEnumerable<OperationTransformer> GetTransformers() => InternalTransformers;

        public IEnumerable<PersisterFactory> GetEmitters() => InternalEmitters;
    }
}