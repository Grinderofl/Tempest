using System.Collections.Generic;

namespace Tempest.Core.Operations.Execution
{
    public interface IOperationExecutor
    {
        void Execute(IEnumerable<Operation> operations);
    }
}