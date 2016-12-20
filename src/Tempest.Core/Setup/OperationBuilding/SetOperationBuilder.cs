using Tempest.Core.Scaffolding;

namespace Tempest.Core.Setup.OperationBuilding
{
    public class SetOperationBuilder : OperationBuilderBase
    {
        public SetOperationBuilder(ScaffoldingConfiguration configuration) : base(configuration)
        {
        }

        public SetOperationBuilder TargetSubDirectory(string relativePath)
        {
            Configuration.TargetSubDirectory = relativePath;
            return this;
        }
    }
}