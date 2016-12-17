using System.IO;

namespace Tempest.Core.Setup.Sourcing
{
    // Lots of inheritance possibilities here
    public class TemplateFileSourceGenerator : FileSourceGenerator
    {
        public TemplateFileSourceGenerator(string relativePath) : base(relativePath)
        {
        }

        protected override string AssembleFilePath(SourcingContext context, string relativePath)
            => Path.Combine(context.TemplateRoot.FullName, relativePath);
    }
}