using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tempest.Configuration;

namespace Tempest.Runner.Impl
{
    public class DirectoryFinder : IDirectoryFinder
    {
        //public DirectoryInfo FindGeneratorLibraryDirectory()
        //{
        //    var codeBase = typeof(TempestRunner).GetTypeInfo().Assembly.CodeBase;
        //    var builder = new UriBuilder(codeBase);
        //    var filePath = Uri.UnescapeDataString(builder.Path);
        //    var path = Path.Combine(Path.GetDirectoryName(filePath), "Generators");
        //    if(Directory.Exists(path))
        //        return new DirectoryInfo(path);

        //    // Technically should try to find from nuget
        //    throw new Exception("No generators installed");
        //}

        private readonly ITempestConfigurationService _configurationService;

        public DirectoryFinder(ITempestConfigurationService configurationService)
        {
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            _configurationService = configurationService;
        }

        public DirectoryInfo FindTempestExecutableDirectory()
        {
            var codeBase = typeof(TempestRunner).GetTypeInfo().Assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var filePath = Uri.UnescapeDataString(uri.Path);
            return new DirectoryInfo(Path.GetDirectoryName(filePath));
        }
        
        public IEnumerable<DirectoryInfo> FindGeneratorDirectories()
        {
            var defaultDirectory = FindTempestExecutableDirectory().GetDirectories("Generators").First();
            yield return defaultDirectory;
            
            foreach(var path in _configurationService.GetGeneratorPaths())
                yield return new DirectoryInfo(path);
        }

        public DirectoryInfo FindWorkingDirectory() => new DirectoryInfo(Directory.GetCurrentDirectory());
    }
}