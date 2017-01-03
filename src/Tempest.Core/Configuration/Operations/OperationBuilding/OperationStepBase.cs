using System;
using System.Collections.Generic;
using Tempest.Core.Configuration.Operations.Persistence;
using Tempest.Core.Configuration.Operations.Sourcing;
using Tempest.Core.Configuration.Operations.Transformation;
using Tempest.Core.Operations;

namespace Tempest.Core.Configuration.Operations.OperationBuilding
{
    public abstract class OperationStepBase
    {
        private readonly Func<ScaffoldOperationConfiguration, SourceFactory> _sourceFactoryFunc;
        private readonly SourceFactory _sourceFactory;
        protected IList<PersisterFactoryBase> InternalEmitters = new List<PersisterFactoryBase>();
        protected IList<OperationTransformerBase> InternalTransformers = new List<OperationTransformerBase>();
        protected TransformationScope TransformationScope = TransformationScope.BeforeGlobals;

        protected OperationStepBase(SourceFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }

        protected OperationStepBase(Func<ScaffoldOperationConfiguration, SourceFactory> sourceFactoryFunc)
        {
            _sourceFactoryFunc = sourceFactoryFunc;
        }

        public SourceFactory GetSource(ScaffoldOperationConfiguration configuration) => _sourceFactoryFunc?.Invoke(configuration) ?? _sourceFactory;

        public IEnumerable<OperationTransformerBase> GetTransformers() => InternalTransformers;

        public IEnumerable<PersisterFactoryBase> GetEmitters() => InternalEmitters;

        public virtual TransformationScope GetScope() => TransformationScope;


    }
}