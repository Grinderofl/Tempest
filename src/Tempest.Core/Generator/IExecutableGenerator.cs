using System.Collections.Generic;
using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Operations;

namespace Tempest.Core.Generator
{
    public interface IExecutableGenerator
    {
        IEnumerable<ConfigurationOption> CreateOptions();
        ScaffoldOperationConfiguration ConfigureOperations(ScaffoldOperationConfiguration configuration);
    }
}