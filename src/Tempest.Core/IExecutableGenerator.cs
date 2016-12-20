using System.Collections.Generic;
using Tempest.Core.Options;
using Tempest.Core.Scaffolding;

namespace Tempest.Core
{
    public interface IExecutableGenerator
    {

        IEnumerable<ConfigurationOption> CreateOptions();
        ScaffoldOperationConfiguration ConfigureOperations(ScaffoldOperationConfiguration configuration);
    }
}