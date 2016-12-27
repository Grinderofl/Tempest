using System;
using System.IO;
using System.Reflection;

namespace Tempest.Boot.Runner.Activation.Impl
{
    public class TempestAssemblyLoader : ITempestAssemblyLoader
    {
        public Assembly Load(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                return null;
            //var codebase = typeof(TempestAssemblyLoader).GetTypeInfo().Assembly.CodeBase;
            //var localLoadContext = new AssemblyLoader(Path.GetDirectoryName(codebase));
            //var localAssembly = localLoadContext.LoadFromAssemblyPath(path);
            //if(localAssembly != null)
            //    return localAssembly;
            
            var loadContext = new AssemblyLoader(Path.GetDirectoryName(path));
            return loadContext.LoadFromAssemblyPath(path);
        }
    }
}