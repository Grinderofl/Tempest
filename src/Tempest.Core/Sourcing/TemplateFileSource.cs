using System;
using System.IO;
using System.Threading.Tasks;

namespace Tempest.Core.Sourcing
{
    // Lots of inheritance possibilities here
    public class TemplateFileSource : FileSource
    {
        public TemplateFileSource(string relativePath) : base(relativePath)
        {
        }

        protected override string AssembleFilePath(SourcingContext context, string relativePath)
            => Path.Combine(context.TemplateRoot.FullName, relativePath);
    }
}