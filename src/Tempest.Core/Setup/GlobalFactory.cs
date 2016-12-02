namespace Tempest.Core.Setup
{
    public class GlobalFactory : BuilderFactoryBase
    {
        public GlobalFactory(GeneratorBase engine) : base(engine)
        {
            Transform = new TransformFactory(Engine);
        }

        public TransformFactory Transform { get; }
    }
}