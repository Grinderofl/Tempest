using Tempest.Core.Sourcing;

namespace Tempest.Core.Setup
{
    /// <summary>
    /// Contains methods that should copy a template file
    /// </summary>
    public class CopyFactory : BuilderFactoryBase
    {
        public CopyFactory(GeneratorBase engine) : base(engine)
        {
        }

        public TemplateStep Template(string filePath) => CreateStep(Sources.FromTemplate(filePath));
    }
}