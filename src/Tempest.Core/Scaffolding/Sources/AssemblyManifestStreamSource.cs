using System.IO;
using System.Reflection;

namespace Tempest.Core.Scaffolding.Sources
{
    public class AssemblyManifestStreamSource : AbstractStreamSource
    {
        private readonly Assembly _assembly;
        private readonly string _resource;

        public AssemblyManifestStreamSource(Assembly assembly, string resource)
        {
            _assembly = assembly;
            _resource = resource;
        }

        public override Stream Create()
        {
            return _assembly.GetManifestResourceStream(_resource);
        }

        protected override string GetStreamDescriptor()
        {
            return $"{_resource} ({_assembly.FullName})";
        }
    }
}