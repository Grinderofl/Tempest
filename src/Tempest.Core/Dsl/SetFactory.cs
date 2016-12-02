namespace Tempest.Core.Dsl
{
    public class SetFactory : BuilderFactoryBase
    {
        public SetFactory(GeneratorBase engine) : base(engine)
        {
        }

        public SetFactory TargetSubDirectory(string relativePath)
        {
            Engine.SetTargetSubDirectory(relativePath);
            return this;
        }
    }
}