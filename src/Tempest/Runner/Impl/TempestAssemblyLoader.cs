using System;
using System.IO;
using System.Reflection;

namespace Tempest.Runner.Impl
{
    public class TempestAssemblyLoader : ITempestAssemblyLoader
    {
        public Assembly Load(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                return null;

            var loadContext = new AssemblyLoader(Path.GetDirectoryName(path));
            return loadContext.LoadFromAssemblyPath(path);
        }
    }
}