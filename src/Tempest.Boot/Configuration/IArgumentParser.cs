﻿namespace Tempest.Boot.Configuration
{
    /// <summary>
    ///     Processes incoming command line arguments into a form Tempest can understand
    /// </summary>
    public interface IArgumentParser
    {
        string[] ParseArguments(string[] args);
    }
}