using Tempest.Core.Sourcing;

namespace Tempest.Core
{
    /// <summary>
    /// Contains methods that create stuff out of thin air.
    /// </summary>
    public class CreateFactory : BuilderFactoryBase
    {
        public CreateFactory(GeneratorEngineBase engine) : base(engine)
        {
        }

        public TemplateStep FromString(string source) => CreateStep(Sources.FromString(source));
    }
}