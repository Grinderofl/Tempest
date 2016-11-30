using System.IO;

namespace Tempest.Core.Sourcing
{
    public class SourcingContext
    {
        public DirectoryInfo TemplateRoot { get; set; }
        public DirectoryInfo TargetRoot { get; set; }
    }
}