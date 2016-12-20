using System;
using Tempest.Core.Scaffolding;

namespace Tempest.Core.Setup.OperationBuilding
{
    public class SetOperationBuilder : OperationBuilderBase
    {
        public SetOperationBuilder TargetSubDirectory(string relativePath)
        {
            Actions.Add(s => s.TargetSubDirectory = relativePath);
            return this;
        }
    }
}