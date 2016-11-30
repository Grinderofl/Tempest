using Tempest.Core.Sourcing;

namespace Tempest.Core
{
    /// <summary>
    /// Contains methods that create stuff out of thin air.
    /// </summary>
    public class CreateFactory
    {
        
        public TemplateStep FromString(string source) => new TemplateStep(Sources.FromString(source));
    }
}