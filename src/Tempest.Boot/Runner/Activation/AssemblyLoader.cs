using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace Tempest.Boot.Runner.Activation
{
    public class AssemblyLoader : AssemblyLoadContext
    {
        private readonly string _basePath;

        public AssemblyLoader(string basePath)
        {
            _basePath = basePath;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            //var dependencies = DependencyContext.Default;
            //var resources = dependencies.CompileLibraries.Where(c => c.Name.Contains(assemblyName.FullName)).ToList();
            //if (resources.Any())
            //    return Assembly.Load(new AssemblyName(resources.First().Name));

            var tempestPath = typeof(AssemblyLoader).GetTypeInfo().Assembly.CodeBase;
            var tempestDir = Path.GetDirectoryName(tempestPath);
            var tempestDirFileInfo = new FileInfo(Path.Combine(tempestDir, $"{assemblyName.Name}.dll"));
            if (tempestDirFileInfo.Exists)
            {
                var loader = new AssemblyLoader(tempestDir);
                var loadedAssembly = loader.LoadFromAssemblyPath(tempestDirFileInfo.FullName);
                if(loadedAssembly != null)
                    return loadedAssembly;
            }


            var fileInfo = new FileInfo(Path.Combine(_basePath, $"{assemblyName.Name}.dll"));
            if (File.Exists(fileInfo.FullName))
            {
                var loader = new AssemblyLoader(fileInfo.DirectoryName);
                return loader.LoadFromAssemblyPath(fileInfo.FullName);
            }

            return Assembly.Load(assemblyName);
        }
    }
}