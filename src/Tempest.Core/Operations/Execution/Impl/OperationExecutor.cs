using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Tempest.Core.Operations.Execution.Impl
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly ILogger<OperationExecutor> _logger;
        private readonly GeneratorContext _context;

        public OperationExecutor(ILogger<OperationExecutor> logger, GeneratorContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Execute(IEnumerable<Operation> operations)
        {
            var shouldLogOperation = _context.ShouldLogOperation();
            foreach (var operation in operations)
            {
                if(shouldLogOperation)
                    _logger.LogInformation($"Scaffolding: {operation.Describe()}");

                operation.Execute();
            }
        }
    }
}