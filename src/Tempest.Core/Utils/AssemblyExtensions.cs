using System;
using System.IO;
using System.Reflection;

namespace Tempest.Core.Utils
{
    public static class AssemblyExtensions
    {
        public static DirectoryInfo GetAssemblyDirectory(this Assembly assembly)
        {
            var assemblyUri = new UriBuilder(assembly.CodeBase);
            var path = Uri.UnescapeDataString(assemblyUri.Path);
            return new DirectoryInfo(Path.GetDirectoryName(path));
        }
    }
}