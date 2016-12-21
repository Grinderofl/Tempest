using System.IO;
using System.Reflection;

namespace Tempest.Core.Operations.Providers
{
    public class AssemblyManifestStreamProvider : AbstractStreamProvider
    {
        private readonly Assembly _assembly;
        private readonly string _resource;

        public AssemblyManifestStreamProvider(Assembly assembly, string resource)
        {
            _assembly = assembly;
            _resource = resource;
        }

        public override Stream Provide()
        {
            return _assembly.GetManifestResourceStream(_resource);
        }

        protected override string GetStreamDescriptor()
        {
            return $"{_resource} ({_assembly.FullName})";
        }
    }
}