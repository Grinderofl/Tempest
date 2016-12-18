using System;
using Tempest.Core.Setup.Sourcing;

namespace Tempest.Core.Setup.OperationBuilding
{
    public abstract class OperationBuilderBase
    {
        protected readonly GeneratorBase Engine;

        protected OperationBuilderBase(GeneratorBase engine)
        {
            if (engine == null) throw new ArgumentNullException(nameof(engine));
            Engine = engine;
        }

        protected virtual OperationStep CreateStep(SourceFactory sourceFactory)
        {
            var step = new OperationStep(sourceFactory);
            Engine.Steps.Add(step);
            return step;
        }
    }
}