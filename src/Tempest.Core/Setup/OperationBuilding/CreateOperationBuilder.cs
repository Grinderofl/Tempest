using Tempest.Core.Setup.Sourcing;

namespace Tempest.Core.Setup.OperationBuilding
{
    /// <summary>
    ///     Contains methods that create stuff out of thin air.
    /// </summary>
    public class CreateOperationBuilder : OperationBuilderBase
    {
        public CreateOperationBuilder(GeneratorBase engine) : base(engine)
        {
        }

        public OperationStep FromString(string source) => CreateStep(Sources.FromString(source));
    }
}