using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Tempest.Core.Operations.Execution.Impl
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly ILogger<OperationExecutor> _logger;
        
        public OperationExecutor(ILogger<OperationExecutor> logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
        }

        public virtual void Execute(IEnumerable<Operation> operations)
        {
            foreach (var operation in operations)
            {
                if(_logger.IsEnabled(LogLevel.Information))
                    _logger.LogInformation($"Scaffolding: {operation.Describe()}");

                operation.Execute();
            }
        }
    }
}