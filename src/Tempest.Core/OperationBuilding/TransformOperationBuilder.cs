using Tempest.Core.Setup.Transformation;

namespace Tempest.Core.OperationBuilding
{
    public class TransformOperationBuilder : OperationBuilderBase
    {
        public TransformOperationBuilder(GeneratorBase engine) : base(engine)
        {
        }

        public TransformOperationBuilder NoOp()
        {
            Engine.GlobalTransformers.Add(Transformers.NoOp);
            return this;
        }

        public TransformOperationBuilder Empty()
        {
            Engine.GlobalTransformers.Add(Transformers.Empty);
            return this;
        }

        public TransformOperationBuilder Token(string token, string replaceWith)
        {
            Engine.GlobalTransformers.Add(Transformers.Token(token, replaceWith));
            return this;
        }
    }
}