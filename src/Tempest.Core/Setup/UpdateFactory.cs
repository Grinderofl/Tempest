using Tempest.Core.Sourcing;

namespace Tempest.Core.Setup
{
    public class UpdateFactory : BuilderFactoryBase
    {
        // Methods that read an existing file in output directory
        public UpdateFactory(GeneratorBase engine) : base(engine)
        {
        }

        public ScaffoldStep File(string filePath) => CreateStep(Sources.FromTarget(filePath));
    }
}