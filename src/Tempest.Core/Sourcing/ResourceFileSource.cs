using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Tempest.Core.Sourcing
{
    public class ResourceFileSource : Source
    {
        private readonly string _resourcePath;
        private readonly Assembly _resourceAssembly;

        public ResourceFileSource(string resourcePath, Assembly resourceAssembly)
        {
            if (resourcePath == null) throw new ArgumentNullException(nameof(resourcePath));
            if (resourceAssembly == null) throw new ArgumentNullException(nameof(resourceAssembly));
            _resourcePath = resourcePath;
            _resourceAssembly = resourceAssembly;
        }

        //protected override Stream GenerateCore(SourcingContext context) => 
        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {

            //var fileName = Path.GetFileName(_resourcePath);
            var match = Regex.Match(_resourcePath, @"\.[^\.]*\.", RegexOptions.RightToLeft);
            int ix = match.Success ? match.Index : -1;
            var fileName = _resourcePath.Substring(ix);
            yield return new SourcingResult()
            {
                FileName = fileName,
                OutputStream = _resourceAssembly.GetManifestResourceStream(_resourcePath)
            };
        }
    }
}