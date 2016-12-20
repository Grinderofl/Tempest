using Tempest.Core.Scaffolding;
using Tempest.Core.Setup.Transformation;

namespace Tempest.Core.Setup.OperationBuilding
{
    public class TransformOperationBuilder : OperationBuilderBase
    {
        public TransformOperationBuilder(ScaffoldingConfiguration configuration) : base(configuration)
        {
        }

        public TransformOperationBuilder NoOp()
        {
            Configuration.GlobalTransformers.Add(Transformers.NoOp);
            return this;
        }

        public TransformOperationBuilder Empty()
        {
            Configuration.GlobalTransformers.Add(Transformers.Empty);
            return this;
        }

        public TransformOperationBuilder Token(string token, string replaceWith)
        {
            Configuration.GlobalTransformers.Add(Transformers.Token(token, replaceWith));
            return this;
        }
    }
}