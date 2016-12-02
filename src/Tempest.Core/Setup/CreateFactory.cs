using Tempest.Core.Sourcing;

namespace Tempest.Core.Setup
{
    /// <summary>
    /// Contains methods that create stuff out of thin air.
    /// </summary>
    public class CreateFactory : BuilderFactoryBase
    {
        public CreateFactory(GeneratorBase engine) : base(engine)
        {
        }

        public TemplateStep FromString(string source) => CreateStep(Sources.FromString(source));
    }
}