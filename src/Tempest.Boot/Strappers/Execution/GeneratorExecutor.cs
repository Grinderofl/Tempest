using System;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Core.Scaffolding;

namespace Tempest.Boot.Strappers.Execution
{
    public class GeneratorExecutor : IBootstrapperExecutor
    {
        public int Execute(IServiceProvider provider)
        {
            var scaffolder = provider.GetService<IScaffolder>();
            scaffolder.Scaffold();
            return 0;
        }
    }
}