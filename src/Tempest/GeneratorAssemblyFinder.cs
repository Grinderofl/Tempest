using System;
using System.IO;
using System.Reflection;

namespace Tempest
{
    public class GeneratorAssemblyFinder : IGeneratorAssemblyFinder
    {
        private readonly IDirectoryFinder _directoryFinder;

        public GeneratorAssemblyFinder(IDirectoryFinder directoryFinder)
        {
            if (directoryFinder == null) throw new ArgumentNullException(nameof(directoryFinder));
            _directoryFinder = directoryFinder;
        }

        public Assembly FindByName(string generatorName)
        {
            var generatorDirectory = FindGeneratorDirectory(generatorName);
            return FindAssembly(generatorDirectory, generatorName);
        }

        private Assembly FindAssembly(string generatorDirectory, string generatorName)
        {
            var assemblyPath = Path.Combine(generatorDirectory, $"Tempest.Generator.{generatorName}.dll");
            if (!File.Exists(assemblyPath))
                throw new FileNotFoundException(
                    $"The assembly for generator {generatorName} could not be found. Searched location: '{assemblyPath}'");
            var loader = new AssemblyLoader(generatorDirectory);
            var assembly = loader.LoadFromAssemblyPath(assemblyPath);
            if(assembly == null)
                throw new NullReferenceException($"The assembly {assemblyPath} could not be loaded.");
            return assembly;
        }
        
        private string FindGeneratorDirectory(string name)
        {
            var generatorsDirectory = _directoryFinder.FindGeneratorLibraryDirectory();
            var generatorNamePattern = $"Tempest.Generator.{name}";
            var generatorDirectory = Path.Combine(generatorsDirectory.FullName, generatorNamePattern);
            if (!Directory.Exists(generatorDirectory))
            {
                throw new DirectoryNotFoundException(
                    $"The plugin {name} directory could not be found. Searched location: '{generatorDirectory}'");
            }
            return generatorDirectory;
        }
    }
}