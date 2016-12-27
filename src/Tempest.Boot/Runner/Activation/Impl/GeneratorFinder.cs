using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tempest.Boot.Configuration;
using Tempest.Boot.Utils;
using Tempest.Core;
using Tempest.Core.Generator;

namespace Tempest.Boot.Runner.Activation.Impl
{
    // TODO This class is doing too much...
    public class GeneratorFinder : IGeneratorFinder
    {
        
        private readonly string[] _assemblyPatterns =
        {
            "Tempest.Generator.{0}.dll",
            "{0}.dll"
        };

        private readonly string[] _generatorTypeNamePatterns =
        {
            "{0}Generator",
            "{0}"
        };

        private readonly string[] _relativeSearchPaths =
        {
            "{0}/Generators/Tempest.Generator.{1}", // Root/Generators/Tempest.Generator.GeneratorName/
            "{0}/Generators/{1}", // Root/Generators/GeneratorName/
            "{0}/Tempest.Generator.{1}", // Root/Tempest.Generator.GeneratorName/
            "{0}/{1}", // Root/GeneratorName/
            "{0}" // Root/ 
        };

        private readonly ITempestAssemblyLoader _tempestAssemblyLoader;
        private readonly TempestConfiguration _configuration;

        public GeneratorFinder(ITempestAssemblyLoader tempestAssemblyLoader, TempestConfiguration configuration)
        {
            if (tempestAssemblyLoader == null) throw new ArgumentNullException(nameof(tempestAssemblyLoader));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _tempestAssemblyLoader = tempestAssemblyLoader;
            _configuration = configuration;
        }

        public Type LocateGenerator(DirectoryInfo[] directoriesToSearch, string generatorName)
        {
            // Attempts to go through the following directories in order
            // Pattern:
            //

            // To find a following DLL:
            // Tempest.Generator.GeneratorName.dll
            // GeneratorName.dll


            // Generator type determined by convention:
            // Tempest.Generator.GeneratorName
            // GeneratorNameGenerator
            // GeneratorName


            var foundAssemblies = FindCandidateAssemblies(directoriesToSearch, generatorName);
            foreach (var assembly in foundAssemblies)
            {
                var types =
                    assembly.ExportedTypes.Where(
                            t =>
                                t.IsConcrete() &&
                                t.Implements(typeof(IExecutableGenerator)))
                        .ToArray();

                if (!types.Any()) continue;

                foreach (var typename in _generatorTypeNamePatterns.Select(n => string.Format(n, generatorName)))
                {
                    var foundType = types.FirstOrDefault(t => t.Name.Equals(typename, StringComparison.OrdinalIgnoreCase));
                    if (foundType != null)
                        return foundType;
                }
            }

            if (_configuration.AutoInstallGenerators)
            {
                // Install generators here.
                // var locatedGenerator = _generatorInstaller.InstallGenerator(loaderContext.Name);
                // if(locatedGenerator != null)
                //      return locatedGenerator;
            }


            return null;
        }

        public IEnumerable<Type> LocateGenerators(DirectoryInfo[] directoriesToSearch)
        {
            var directoriesWithCombined =
                directoriesToSearch.Union(directoriesToSearch.SelectMany(x => x.GetDirectories())).ToArray();

            foreach (var generatorDir in directoriesWithCombined)
                foreach (var file in generatorDir.EnumerateFiles("*.dll"))

                {
                    var loadedAssembly = _tempestAssemblyLoader.Load(file.FullName);
                    if (loadedAssembly != null)
                    {
                        var concreteTypes = loadedAssembly.ExportedTypes.Where(t => t.IsConcrete()).ToList();
                        //var generatorTypes = concreteTypes.Where(t => t.IsSubclassOf(typeof(GeneratorBase))).ToList();
                        var generatorTypes = concreteTypes.Where(t => t.GetTypeInfo()
                            .ImplementedInterfaces.Any(i => i.Name == "IExecutableGenerator")).ToList();

                        var types = generatorTypes.ToArray();

                        if (!types.Any()) continue;
                        foreach (var type in types)
                            yield return type;
                    }
                }
        }

        private IEnumerable<Assembly> FindCandidateAssemblies(DirectoryInfo[] directoriesToSearch, string generatorName)
        {
            if ((directoriesToSearch == null) || string.IsNullOrWhiteSpace(generatorName)) yield break;

            foreach (var dir in directoriesToSearch)
                foreach (var relativeSearchPath in _relativeSearchPaths)
                {
                    var absoluteGeneratorSearchPath = string.Format(relativeSearchPath, dir.FullName, generatorName);
                    if (!Directory.Exists(absoluteGeneratorSearchPath)) continue;

                    foreach (var assemblyPattern in _assemblyPatterns)
                    {
                        var assemblyDllName = string.Format(assemblyPattern, generatorName);
                        var assemblyPath = Path.Combine(absoluteGeneratorSearchPath, assemblyDllName);
                        if (!File.Exists(assemblyPath)) continue;

                        var assembly = _tempestAssemblyLoader.Load(assemblyPath);
                        if (assembly != null)
                            yield return assembly;
                    }
                }
        }
    }
}