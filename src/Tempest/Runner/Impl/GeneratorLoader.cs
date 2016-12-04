using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tempest.Configuration;
using Tempest.Core;

namespace Tempest.Runner.Impl
{
    public class GeneratorLoader : IGeneratorLoader
    {
        // Generator loader
        // Finds the DLL with the generator name
        // Loads the DLL
        // Loads the Generator type

        //private readonly IGeneratorAssemblyFinder _generatorAssemblyFinder;

        //public GeneratorLoader(IGeneratorAssemblyFinder generatorAssemblyFinder)
        //{
        //    _generatorAssemblyFinder = generatorAssemblyFinder;
        //}

        //public GeneratorEngineBase FindGeneratorByName(string generatorName)
        //{
        //    Assembly generatorAssembly = null;
        //    Type generatorType = null;

        //    try
        //    {
        //        generatorAssembly = _generatorAssemblyFinder.FindByName(generatorName);
        //        generatorType = FindGeneratorType(generatorAssembly, generatorName);
        //    }
        //    catch (FileNotFoundException)
        //    {

        //    }


        //    if(generatorType == null)
        //        throw new GeneratorNotFoundException(generatorName, generatorAssembly);

        //    return Activator.CreateInstance(generatorType) as GeneratorEngineBase;
        //}

        //private Type FindGeneratorType(Assembly generatorAssembly, string generatorName)
        //{
        //    var implementsGeneratorType =
        //        generatorAssembly.ExportedTypes.Where(x => IntrospectionExtensions.GetTypeInfo(x).IsSubclassOf(typeof(GeneratorBase)));

        //    var containsName = implementsGeneratorType.Where(x => x.Name.Contains(generatorName));
        //    return containsName.FirstOrDefault();
        //}
        private readonly IDirectoryFinder _directoryFinder;
        private readonly IGeneratorLocator _locator;

        // Temporary. Redo into strongly typed configuration through DI.
        private readonly ITempestConfigurationService _configurationService;

        public GeneratorLoader(IDirectoryFinder directoryFinder, IGeneratorLocator locator, ITempestConfigurationService configurationService)
        {
            if (directoryFinder == null) throw new ArgumentNullException(nameof(directoryFinder));
            if (locator == null) throw new ArgumentNullException(nameof(locator));
            _directoryFinder = directoryFinder;
            _locator = locator;
            _configurationService = configurationService;
        }

        private IEnumerable<DirectoryInfo> SearchableDirectories(LoaderContext loaderContext)
        {
            if (!string.IsNullOrEmpty(loaderContext.AdditionalSearchPath))
                yield return new DirectoryInfo(loaderContext.AdditionalSearchPath);

            foreach (var dir in _directoryFinder.FindGeneratorDirectories())
                yield return dir;
        }

        public GeneratorEngineBase Load(LoaderContext loaderContext)
        {
            var directoriesToSearch = SearchableDirectories(loaderContext).ToArray();
            var locatedGenerator = _locator.Locate(directoriesToSearch, loaderContext.Name);
            if (locatedGenerator == null && _configurationService.ShouldInstallGeneratorsAutomatically())
            {
                // Install generator here. todo
                // locatedGenerator = _generatorInstaller.InstallGenerator(loaderContext.Name);
            }
            
            if(locatedGenerator == null)
                throw new GeneratorNotFoundException($"The generator '{loaderContext.Name} could not be found.' Searched locations: '{string.Join("', '", directoriesToSearch.Select(x => x.FullName))}'");

            return (GeneratorEngineBase) Activator.CreateInstance(locatedGenerator);

        }


    }
}