using System.Collections.Generic;
using Tempest.Core.Setup.OperationBuilding;
using Tempest.Core.Setup.Transformation;

namespace Tempest.Core.Operations
{
    public class ScaffoldOperationConfiguration
    {
        public IList<OperationStep> Steps { get; } = new List<OperationStep>();
        public IList<OperationTransformer> GlobalTransformers { get; } = new List<OperationTransformer>();
        public string TargetSubDirectory { get; set; }
    }
}