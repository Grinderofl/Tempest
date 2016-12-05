using System.Collections.Generic;
using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Sourcing
{
    public class TemplateGlobSource : Source
    {
        private readonly string _globPattern;

        public TemplateGlobSource(string globPattern)
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
                yield return new SourcingResult()
                {
                    FileName = file.Name,
                    OutputStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read)
                };
            }
        }
    }
}