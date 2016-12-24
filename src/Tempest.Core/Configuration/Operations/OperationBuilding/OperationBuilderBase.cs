using System;
using System.Collections.Generic;
using Tempest.Core.Configuration.Operations.Sourcing;
using Tempest.Core.Operations;

namespace Tempest.Core.Configuration.Operations.OperationBuilding
{
    public abstract class OperationBuilderBase
    {
        protected readonly IList<Action<ScaffoldOperationConfiguration>> Actions =
            new List<Action<ScaffoldOperationConfiguration>>();

        protected virtual OperationStep CreateStep(SourceFactory sourceFactory)
        {
            if (sourceFactory == null) throw new ArgumentNullException(nameof(sourceFactory));
            var step = new OperationStep(sourceFactory);
            Actions.Add(conf => conf.Steps.Add(step));
            return step;
        }

        public virtual void VisitConfiguration(ScaffoldOperationConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            foreach (var action in Actions)
            {
                action(configuration);
            }
        }
    }
}