using System.IO;
using System.Reflection;

namespace Tempest.Core.Domain.Streaming
{
    public class AssemblyManifestStreamFactory : AbstractStreamFactory
    {
        private readonly Assembly _assembly;
        private readonly string _resource;

        public AssemblyManifestStreamFactory(Assembly assembly, string resource)
        {
            _assembly = assembly;
            _resource = resource;
        }

        public override Stream Create()
        {
            return _assembly.GetManifestResourceStream(_resource);
        }
    }
}