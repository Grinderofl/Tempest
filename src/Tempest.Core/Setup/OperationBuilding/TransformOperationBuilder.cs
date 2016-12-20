using Tempest.Core.Scaffolding;
using Tempest.Core.Setup.Transformation;

namespace Tempest.Core.Setup.OperationBuilding
{
    public class TransformOperationBuilder : OperationBuilderBase
    {
        public TransformOperationBuilder NoOp()
        {
            Actions.Add(s => s.GlobalTransformers.Add(Transformers.NoOp));
            return this;
        }

        public TransformOperationBuilder Empty()
        {
            Actions.Add(s => s.GlobalTransformers.Add(Transformers.Empty));
            return this;
        }

        public TransformOperationBuilder Token(string token, string replaceWith)
        {
            Actions.Add(s => s.GlobalTransformers.Add(Transformers.Token(token, replaceWith)));
            return this;
        }
    }
}