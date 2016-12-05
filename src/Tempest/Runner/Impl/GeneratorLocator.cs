using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tempest.Core;

namespace Tempest.Runner.Impl
{
    public class GeneratorLocator : IGeneratorLocator
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

        public GeneratorLocator(ITempestAssemblyLoader tempestAssemblyLoader)
        {
            _tempestAssemblyLoader = tempestAssemblyLoader;
        }

        public Type Locate(DirectoryInfo[] directoriesToSearch, string generatorName)
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
                                t.IsSubclassOf(typeof(GeneratorEngineBase)))
                        .ToArray();

                if (!types.Any()) continue;

                foreach (var typename in _generatorTypeNamePatterns.Select(n => string.Format(n, generatorName)))
                {
                    var foundType = types.FirstOrDefault(t => t.Name.Equals(typename));
                    if (foundType != null)
                        return foundType;
                }
            }
            return null;
        }

        public IEnumerable<Type> Locate(DirectoryInfo[] directoriesToSearch)
        {
            foreach (var dir in directoriesToSearch)
                if (dir.Name.Equals("Generators"))
                    foreach (var generatorDir in dir.GetDirectories())
                        foreach (
                            var file in
                            generatorDir.EnumerateFiles("*.dll")
                                .Where(x => x.Name.Contains("Generator") && (x.Name != "Generators")))
                        {
                            var loadedAssembly = _tempestAssemblyLoader.Load(file.FullName);
                            if (loadedAssembly != null)
                            {
                                var types =
                                    loadedAssembly.ExportedTypes.Where(
                                            t =>
                                                t.IsConcrete() &&
                                                t.IsSubclassOf(typeof(GeneratorEngineBase)))
                                        .ToArray();

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