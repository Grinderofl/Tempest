using System;

namespace Tempest.Boot.Strappers.Execution
{
    public interface IBootstrapperExecutor
    {
        int Execute(IServiceProvider provider);
    }
}