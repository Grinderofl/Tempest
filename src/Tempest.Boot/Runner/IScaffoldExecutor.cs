using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core;

namespace Tempest.Boot.Runner
{
    public interface IScaffoldExecutor
    {
        void Execute(ScaffolderConfigurer scaffold);
    }
}
