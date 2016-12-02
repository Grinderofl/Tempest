namespace Tempest.Core.Dsl
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