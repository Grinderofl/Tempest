using System.IO;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    public class SourcingContext
    {
        public DirectoryInfo TemplateRoot { get; set; }
        public DirectoryInfo TargetRoot { get; set; }
    }
}