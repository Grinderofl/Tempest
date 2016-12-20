using System;
using Tempest.Core.Scaffolding;
using Tempest.Core.Setup.Sourcing;

namespace Tempest.Core.Setup.OperationBuilding
{
    public abstract class OperationBuilderBase
    {
        protected readonly ScaffoldingConfiguration Configuration;

        protected OperationBuilderBase(ScaffoldingConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            Configuration = configuration;
        }

        protected virtual OperationStep CreateStep(SourceFactory sourceFactory)
        {
            var step = new OperationStep(sourceFactory);
            Configuration.Steps.Add(step);
            return step;
        }
    }
}