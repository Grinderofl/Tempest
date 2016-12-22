using System.Collections.Generic;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Configuration.Operations.Transformation;

namespace Tempest.Core.Operations
{
    public class ScaffoldOperationConfiguration
    {
        public IList<OperationStep> Steps { get; } = new List<OperationStep>();
        public IList<OperationTransformer> GlobalTransformers { get; } = new List<OperationTransformer>();
        public string TargetSubDirectory { get; set; } = "";
    }
}