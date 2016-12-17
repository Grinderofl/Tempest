using System.IO;

namespace Tempest.Core.Setup.Sourcing
{
    public class TargetFileSourceGenerator : FileSourceGenerator
    {
        public TargetFileSourceGenerator(string relativePath) : base(relativePath)
        {
        }

        protected override string AssembleFilePath(SourcingContext context, string relativePath)
            => Path.Combine(context.TargetRoot.FullName, relativePath);
    }
}