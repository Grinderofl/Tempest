using Tempest.Core.Operations;

namespace Tempest.Core.Setup.OperationBuilding
{
    public class GlobalOperationBuilder : OperationBuilderBase
    {
        public TransformOperationBuilder Transform { get; } = new TransformOperationBuilder();

        public TransformOperationBuilder TransformToken(string token, string replaceWith) => Transform.Token(token, replaceWith);

        public override void VisitConfiguration(ScaffoldOperationConfiguration configuration)
        {
            Transform.VisitConfiguration(configuration);
            base.VisitConfiguration(configuration);
        }
    }
}