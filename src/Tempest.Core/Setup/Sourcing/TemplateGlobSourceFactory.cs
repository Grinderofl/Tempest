using System.Collections.Generic;
using System.IO;
using Tempest.Core.Scaffolding.Sources;
using Tempest.Core.Utils;

namespace Tempest.Core.Setup.Sourcing
{
    // Lots of inheritance possibilities here
    public class TemplateGlobSourceFactory : SourceFactory
    {
        private readonly string _globPattern;

        public TemplateGlobSourceFactory(string globPattern)
        {
            _globPattern = globPattern;
        }

        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            var glob = context.TemplateRoot.FullName + _globPattern;
            var files = Glob.Expand(glob);
            foreach (var file in files)
            {
                if (!File.Exists(file.FullName))
                    continue;

                var fileRelativePath = file.FullName.Replace(context.TemplateRoot.FullName, "");
                yield return new SourcingResult
                {
                    FilePath = fileRelativePath,
                    FileName = fileRelativePath,
                    Source = new OpenFileStreamSource(file.FullName)
                };
            }
        }
    }
}