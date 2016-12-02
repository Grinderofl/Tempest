namespace Tempest.Core.Setup
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