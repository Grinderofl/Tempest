using Tempest.Core.Configuration.Operations.Sourcing;

namespace Tempest.Core.Configuration.Operations.OperationBuilding
{
    /// <summary>
    ///     Contains methods that create stuff out of thin air.
    /// </summary>
    public class CreateOperationBuilder : OperationBuilderBase
    {

        public OperationStep FromString(string source) => CreateStep(Sources.FromString(source));
    }
}