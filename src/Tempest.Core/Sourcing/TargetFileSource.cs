using System.IO;

namespace Tempest.Core.Sourcing
{
    public class TargetFileSource : FileSource
    {
        public TargetFileSource(string relativePath) : base(relativePath)
        {
        }

        protected override string AssembleFilePath(SourcingContext context, string relativePath)
            => Path.Combine(context.TargetRoot.FullName, relativePath);
    }
}