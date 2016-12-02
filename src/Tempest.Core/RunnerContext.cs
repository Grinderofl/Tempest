﻿using System.IO;

namespace Tempest.Core
{
    public class RunnerContext
    {
        public string GeneratorName { get; set; }
        public string[] Arguments { get; set; }
        public DirectoryInfo WorkingDirectory { get; set; }
        public DirectoryInfo TempestDirectory { get; set; }
    }
}