using System;
using System.IO;
using System.Reflection;

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

        protected override Stream GenerateCore(SourcingContext context) => _resourceAssembly.GetManifestResourceStream(_resourcePath);
    }
}