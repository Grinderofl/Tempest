using System.Collections.Generic;
using Tempest.Core.Operations;
using Tempest.Core.Options;

namespace Tempest.Core
{
    public interface IExecutableGenerator
    {

        IEnumerable<ConfigurationOption> CreateOptions();
        ScaffoldOperationConfiguration ConfigureOperations(ScaffoldOperationConfiguration configuration);
    }
}