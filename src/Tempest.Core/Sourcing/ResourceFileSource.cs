using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Tempest.Core.Sourcing
{
    // Lots of inheritance possibilities here
    public class ResourceFileSource : Source
    {
        private readonly Assembly _resourceAssembly;
        private readonly string _resourcePath;

        public ResourceFileSource(string resourcePath, Assembly resourceAssembly)
        {
            if (resourcePath == null) throw new ArgumentNullException(nameof(resourcePath));
            if (resourceAssembly == null) throw new ArgumentNullException(nameof(resourceAssembly));
            _resourcePath = resourcePath;
            _resourceAssembly = resourceAssembly;
        }

        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            var match = Regex.Match(_resourcePath, @"\.[^\.]*\.", RegexOptions.RightToLeft);
            var ix = match.Success ? match.Index : -1;
            var fileName = _resourcePath.Substring(ix);
            yield return new SourcingResult
            {
                FileName = fileName,
                OutputStream = _resourceAssembly.GetManifestResourceStream(_resourcePath)
            };
        }
    }
}