using Tempest.Core.Scaffolding;

namespace Tempest.Core.Setup.OperationBuilding
{
    public class GlobalOperationBuilder : OperationBuilderBase
    {
        public GlobalOperationBuilder(ScaffoldingConfiguration configuration) : base(configuration)
        {
            Transform = new TransformOperationBuilder(Configuration);
        }

        public TransformOperationBuilder Transform { get; }

        public TransformOperationBuilder TransformToken(string token, string replaceWith) => Transform.Token(token, replaceWith);
    }
}