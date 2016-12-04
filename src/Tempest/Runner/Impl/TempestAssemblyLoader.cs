using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Tempest.Runner.Impl
{
    public class TempestAssemblyLoader : ITempestAssemblyLoader
    {
        //private readonly AssemblyLoadContext _loadContext;
        private readonly DependencyContext _dependencyContext;

        public TempestAssemblyLoader()
        {
            _dependencyContext = DependencyContext.Default;
            //_loadContext = AssemblyLoadContext.Default;
            
        }

        public Assembly Load(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                return null;

            var loadContext = new AssemblyLoader(Path.GetDirectoryName(path)); //new WrappedAssemblyLoader(_dependencyContext);
            return loadContext.LoadFromAssemblyPath(path);
        }
    }
}