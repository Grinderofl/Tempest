using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core.Operations;

namespace Tempest.Core.Configuration.Scaffolding
{
    public interface IConfigurationResolver
    {
        ScaffoldOperationConfiguration Resolve();
    }


}
