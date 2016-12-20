using Tempest.Core.Scaffolding;
using Tempest.Core.Setup.Sourcing;

namespace Tempest.Core.Setup.OperationBuilding
{
    /// <summary>
    /// Builds operations sourced from target directory
    /// </summary>
    public class UpdateOperationBuilder : OperationBuilderBase
    {
        public UpdateOperationBuilder(ScaffoldingConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Updates an existing file in target directory
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public OperationStep File(string filePath) => CreateStep(Sources.FromTarget(filePath));
    }
}