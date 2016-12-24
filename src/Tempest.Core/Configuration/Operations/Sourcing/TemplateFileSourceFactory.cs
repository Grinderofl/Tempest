using System.IO;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    public class TemplateFileSourceFactory : FileSourceFactory
    {
        public TemplateFileSourceFactory(string relativePath) : base(relativePath)
        {
        }

        protected override string AssembleFilePath(SourcingContext context, string relativePath)
            => Path.Combine(context.TemplateRoot.FullName, relativePath);
    }
}