using System.IO;

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
    }
}