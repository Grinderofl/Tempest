using System;
using System.IO;
using System.Reflection;

namespace Tempest
{
    public class DirectoryFinder : IDirectoryFinder
    {
        public DirectoryInfo FindGeneratorLibraryDirectory()
        {
            var codeBase = typeof(TempestRunner).GetTypeInfo().Assembly.CodeBase;
            var builder = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(builder.Path);
            if(Directory.Exists(path))
                return new DirectoryInfo(path);
            return null;
        }
    }
}