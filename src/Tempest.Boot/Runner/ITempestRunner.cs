﻿using Tempest.Boot.Configuration;

namespace Tempest.Boot.Runner
{
    public interface ITempestRunner
    {
        int Run(TempestRunnerArguments runnerArgs);
    }
}