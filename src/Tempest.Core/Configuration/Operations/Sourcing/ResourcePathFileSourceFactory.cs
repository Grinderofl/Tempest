using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    public class ResourcePathFileSourceFactory : SourceFactory
    {
        private readonly string _resourcePath;
        private readonly Assembly _assembly;
        public ResourcePathFileSourceFactory(string resourcePath, Assembly resourceAssembly)
        {
            _resourcePath = resourcePath;
            _assembly = resourceAssembly;
        }

        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            var resources = _assembly.GetManifestResourceNames();
            foreach (var resource in resources.Where(r => r.StartsWith(_resourcePath)))
            {
                yield return new ResourceFileSourceFactory(resource, _assembly).Generate(context).First();
            }
        }
    }
}