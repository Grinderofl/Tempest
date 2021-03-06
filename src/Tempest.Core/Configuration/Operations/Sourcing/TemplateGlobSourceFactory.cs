using System.Collections.Generic;
using System.IO;
using Tempest.Core.Operations.Providers;
using Tempest.Core.Utils;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
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
                // Maybe should throw here?
                if (!File.Exists(file.FullName))
                    continue;

                var fileRelativePath = file.FullName.Replace(context.TemplateRoot.FullName, "");
                yield return new SourcingResult
                {
                    FilePath = fileRelativePath,
                    FileName = fileRelativePath,
                    Provider = new OpenFileStreamProvider(file.FullName)
                };
            }
        }
    }
}