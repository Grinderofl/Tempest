using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Tempest.Core.Logging;
using Tempest.Core.Options.Rendering;

namespace Tempest.Core.Operations.Execution.Impl
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IEmitter _emitter;
        private readonly RenderOptions _renderOptions;

        public OperationExecutor(IEmitter emitter, RenderOptions renderOptions)
        {
            if (emitter == null) throw new ArgumentNullException(nameof(emitter));
            _emitter = emitter;
            _renderOptions = renderOptions;
        }

        public virtual void Execute(IEnumerable<Operation> operations)
        {
            foreach (var operation in operations)
            {
                // todo: refactor emitter to be more specifically OperationEmitter
                _emitter.SetForegroundColor(_renderOptions.RenderColors[ColorType.EmitterForeground]);
                _emitter.SetBackgroundColor(_renderOptions.RenderColors[ColorType.EmitterBackground]);
                _emitter.Emit($"{operation.Describe()}");
                operation.Execute();
            }
        }
    }
}