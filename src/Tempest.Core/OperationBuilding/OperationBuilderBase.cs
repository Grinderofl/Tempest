using System;
using Tempest.Core.Setup.Sourcing;

namespace Tempest.Core.OperationBuilding
{
    public abstract class OperationBuilderBase
    {
        protected readonly GeneratorBase Engine;

        protected OperationBuilderBase(GeneratorBase engine)
        {
            if (engine == null) throw new ArgumentNullException(nameof(engine));
            Engine = engine;
        }

        protected virtual OperationStep CreateStep(SourceGenerator sourceGenerator)
        {
            var step = new OperationStep(sourceGenerator);
            Engine.Steps.Add(step);
            return step;
        }
    }
}