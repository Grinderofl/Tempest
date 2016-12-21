using Tempest.Core.Configuration.Operations.Sourcing;

namespace Tempest.Core.Configuration.Operations.OperationBuilding
{
    /// <summary>
    /// Builds operations sourced from target directory
    /// </summary>
    public class UpdateOperationBuilder : OperationBuilderBase
    {
        /// <summary>
        /// Updates an existing file in target directory
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public OperationStep File(string filePath) => CreateStep(Sources.FromTarget(filePath));
    }
}