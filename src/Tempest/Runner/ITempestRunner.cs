using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest.Runner
{
    public interface ITempestRunner
    {
        int Run(TempestRunnerArguments runnerArgs);
    }
}
