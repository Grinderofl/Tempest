using System.IO;

namespace Tempest.Core.Setup.Sourcing
{
    public class SourcingContext
    {
        public DirectoryInfo TemplateRoot { get; set; }
        public DirectoryInfo TargetRoot { get; set; }
    }
}