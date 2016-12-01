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
            var filePath = Uri.UnescapeDataString(builder.Path);
            var path = Path.Combine(Path.GetDirectoryName(filePath), "Generators");
            if(Directory.Exists(path))
                return new DirectoryInfo(path);

            // Technically should try to find from nuget
            throw new Exception("No generators installed");
        }
    }
}