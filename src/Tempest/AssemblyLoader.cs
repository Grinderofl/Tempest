using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace Tempest
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
            var dependencies = DependencyContext.Default;
            var resources = dependencies.CompileLibraries.Where(c => c.Name.Contains(assemblyName.FullName)).ToList();
            if (resources.Any())
            {
                return Assembly.Load(new AssemblyName(resources.First().Name));
            }

            var fileInfo = new FileInfo(Path.Combine(_basePath, $"{assemblyName.Name}.dll"));
            if (File.Exists(fileInfo.FullName))
            {
                var loader = new AssemblyLoader(_basePath);
                return loader.LoadFromAssemblyPath(fileInfo.FullName);
            }

            return Assembly.Load(assemblyName);
        }
    }



    public static class TypeExtensions
    {
        public static bool IsConcrete(this Type type) => !type.GetTypeInfo().IsAbstract;
        public static bool IsSubclassOf(this Type type, Type @from) => type.GetTypeInfo().IsSubclassOf(@from);
    }

    //public class DependencyContextAssemblyLoader : AssemblyLoadContext
    //{
    //    protected override Assembly Load(AssemblyName assemblyName)
    //    {
    //        var context = DependencyContext.Default;
    //        var assembliesWithSameName = FindAssemblies(context, assemblyName);
    //    }

    //    private IEnumerable<CompilationLibrary>

    //    private IEnumerable<RuntimeLibrary> FindAssemblies(DependencyContext context, AssemblyName assemblyName)
    //    {
            
    //    }
    //}
}