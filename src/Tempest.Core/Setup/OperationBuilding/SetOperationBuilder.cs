namespace Tempest.Core.Setup.OperationBuilding
{
    public class SetOperationBuilder : OperationBuilderBase
    {
        public SetOperationBuilder(GeneratorBase engine) : base(engine)
        {
        }

        public SetOperationBuilder TargetSubDirectory(string relativePath)
        {
            Engine.SetTargetSubDirectory(relativePath);
            return this;
        }
    }
}