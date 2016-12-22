using System;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Runner;

namespace Tempest.Boot.Strappers.Execution
{
    public class TempestExecutor : IBootstrapperExecutor
    {
        private readonly string[] _arguments;

        public TempestExecutor(string[] arguments)
        {
            _arguments = arguments;
        }

        public int Execute(IServiceProvider provider)
        {
            var executor = provider.GetService<ICommandLineExecutor>();
            return executor.Execute(_arguments);
        }
    }
}