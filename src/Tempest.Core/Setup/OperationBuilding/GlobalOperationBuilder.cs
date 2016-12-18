namespace Tempest.Core.Setup.OperationBuilding
{
    public class GlobalOperationBuilder : OperationBuilderBase
    {
        public GlobalOperationBuilder(GeneratorBase engine) : base(engine)
        {
            Transform = new TransformOperationBuilder(Engine);
        }

        public TransformOperationBuilder Transform { get; }

        public TransformOperationBuilder TransformToken(string token, string replaceWith) => Transform.Token(token, replaceWith);
    }
}