using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Tempest.Core
{
    public class GeneratorContext
    {
        public string GeneratorName { get; set; }
        public string[] Arguments { get; set; }
        public DirectoryInfo WorkingDirectory { get; set; }
        public DirectoryInfo TemplateDirectory { get; set; }
        public DirectoryInfo[] GeneratorDirectories { get; set; }
        public DirectoryInfo TempestDirectory { get; set; }

        public LogLevel LogLevel { get; set; }

        private static readonly LogLevel[] OperationLogLevels =
        {
            LogLevel.Debug,
            LogLevel.Information,
            LogLevel.Trace
        };

        public bool ShouldLogOperation() => OperationLogLevels.Contains(LogLevel);
    }
}