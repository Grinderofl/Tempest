using Tempest.Core.Sourcing;

namespace Tempest.Core
{
    /// <summary>
    /// Contains methods that should copy a template file
    /// </summary>
    public class CopyFactory
    {
        public TemplateStep Template(string filePath) => new TemplateStep(Sources.FromTemplate(filePath));
    }
}