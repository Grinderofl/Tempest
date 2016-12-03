namespace Tempest.Core.Setup
{
    public class GlobalFactory : BuilderFactoryBase
    {
        public GlobalFactory(GeneratorBase engine) : base(engine)
        {
            Transform = new TransformFactory(Engine);
        }

        public TransformFactory Transform { get; }

        public TransformFactory TransformToken(string token, string replaceWith) => Transform.Token(token, replaceWith);
    }
}