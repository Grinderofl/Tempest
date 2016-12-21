using System.IO;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    public class TargetFileSourceFactory : FileSourceFactory
    {
        public TargetFileSourceFactory(string relativePath) : base(relativePath)
        {
        }

        protected override string AssembleFilePath(SourcingContext context, string relativePath)
            => Path.Combine(context.TargetRoot.FullName, relativePath);
    }
}