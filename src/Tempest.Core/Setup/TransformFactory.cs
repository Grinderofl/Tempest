using Tempest.Core.Transformation;

namespace Tempest.Core.Setup
{
    public class TransformFactory : BuilderFactoryBase
    {
        public TransformFactory(GeneratorBase engine) : base(engine)
        {
        }

        public TransformFactory NoOp()
        {
            Engine.GlobalTransformers.Add(Transformers.NoOp);
            return this;
        }

        public TransformFactory Empty()
        {
            Engine.GlobalTransformers.Add(Transformers.Empty);
            return this;
        }

        public TransformFactory Token(string token, string replaceWith)
        {
            Engine.GlobalTransformers.Add(Transformers.Token(token, replaceWith));
            return this;
        }
    }
}