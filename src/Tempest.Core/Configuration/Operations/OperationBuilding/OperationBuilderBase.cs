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
            var step = new OperationStep(sourceFactory);
            Actions.Add(conf => conf.Steps.Add(step));
            return step;
        }

        public virtual void VisitConfiguration(ScaffoldOperationConfiguration configuration)
        {
            foreach (var action in Actions)
            {
                action(configuration);
            }
        }
    }

    // Scaffold configuration is a single instance that is created somewhere
    // and passed into scaffoldconfigurers?
}