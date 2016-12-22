using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tempest.Boot.Configuration;
using Tempest.Boot.Runner.Impl;

namespace Tempest.Boot.Runner.Activation.Impl
{
    // TODO Almost doing too much...
    public class DirectoryFinder : IDirectoryFinder
    {
        private readonly TempestConfiguration _configuration;

        public DirectoryFinder(TempestConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration;
        }

        public DirectoryInfo FindTempestExecutableDirectory()
        {
            var codeBase = typeof(TempestRunner).GetTypeInfo().Assembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var filePath = Uri.UnescapeDataString(uri.Path);
            return new DirectoryInfo(Path.GetDirectoryName(filePath));
        }

        public IEnumerable<DirectoryInfo> FindGeneratorDirectories(string additionalSearchPath = null)
        {
            if (!string.IsNullOrWhiteSpace(additionalSearchPath))
            {
                var additionalDirectory = new DirectoryInfo(additionalSearchPath);
                if (additionalDirectory.Exists)
                    yield return additionalDirectory;
            }
            
            var defaultDirectory = FindTempestExecutableDirectory().GetDirectories("Generators").First();
            yield return defaultDirectory;

            foreach (var path in _configuration.AdditionalPaths)
                yield return new DirectoryInfo(path);
        }

        public DirectoryInfo FindWorkingDirectory() => new DirectoryInfo(Directory.GetCurrentDirectory());
    }
}