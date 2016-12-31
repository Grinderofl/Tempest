using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Tempest.Core.Logging;

namespace Tempest.Core.Operations.Execution.Impl
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IEmitter _emitter;

        public OperationExecutor(IEmitter emitter)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            _emitter = emitter;
        }

        public virtual void Execute(IEnumerable<Operation> operations)
        {
            foreach (var operation in operations)
            {
                _emitter.SetForegroundColor(ConsoleColor.Cyan);
                _emitter.Emit($"Scaffolding {operation.Describe()}");
                operation.Execute();
            }
        }
    }
}