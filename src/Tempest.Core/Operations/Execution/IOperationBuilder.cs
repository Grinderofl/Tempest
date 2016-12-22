using System.Collections.Generic;
using Tempest.Core.Configuration.Operations.Sourcing;

namespace Tempest.Core.Operations.Execution
{
    public interface IOperationBuilder
    {
        IEnumerable<Operation> Build(ScaffoldOperationConfiguration configuration, SourcingContext sourcingContext);
    }
}