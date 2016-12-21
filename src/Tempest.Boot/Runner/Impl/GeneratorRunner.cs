using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using Tempest.Boot.Strapper;
using Tempest.Core;
using Tempest.Core.Operations;
using Tempest.Core.Setup.OperationBuilding;

namespace Tempest.Boot.Runner.Impl
{
    // Tempest Runner does this: parses initial arguments, and searches for the generator
    // It then passes the entire shabang onto a generator executor
    // executor creates a basic bootstrapper and adds core and the assembly of
    // the generator as targets for service discovery
    // The relevant parameters are also added as services
    // then bootstrapper executes the generator itself

    public class GeneratorRunner : IGeneratorRunner
    {
        private readonly IGeneratorActivator _activator;
        private readonly IDirectoryFinder _directoryFinder;
        private readonly IGeneratorLocator _locator;

        public GeneratorRunner(IDirectoryFinder directoryFinder, IGeneratorLocator locator, IGeneratorActivator activator)
        {
            if (directoryFinder == null) throw new ArgumentNullException(nameof(directoryFinder));
            if (locator == null) throw new ArgumentNullException(nameof(locator));
            _directoryFinder = directoryFinder;
            _locator = locator;
            _activator = activator;
        }

        public GeneratorEngineBase Create(LoaderContext loaderContext)
        {
            GeneratorEngineBase result = null;
            var generatorType = loaderContext.Type;
            if (generatorType == null)
            {
                var directoriesToSearch = SearchableDirectories(loaderContext).ToArray();
                generatorType = _locator.Locate(directoriesToSearch, loaderContext.Name);
                if (generatorType == null)
                    throw new GeneratorNotFoundException(
                        $"{loaderContext.Name}, searched locations: '{string.Join("', '", directoriesToSearch.Select(x => x.FullName))}'");
            }
            
            result = _activator.Activate(generatorType);
            return result;
        }

        private IEnumerable<DirectoryInfo> SearchableDirectories(LoaderContext loaderContext)
        {
            if (!string.IsNullOrEmpty(loaderContext.AdditionalSearchPath))
                yield return new DirectoryInfo(loaderContext.AdditionalSearchPath);

            foreach (var dir in _directoryFinder.FindGeneratorDirectories())
                yield return dir;
        }

        public virtual int Run(GeneratorContext loaderContext)
        {
            var configuration = new ScaffoldOperationConfiguration();
            var strapper = new GeneratorBootstrapper(loaderContext.GeneratorType);
            // Find conventions from generator assembly
            strapper.RegisterConvention(new GeneratorRegistrationConvention(loaderContext.GeneratorType));
            strapper.RegisterConvention(new RegisterOperationsConvention(configuration, loaderContext));
            return strapper.Execute(null);
        }
    }
}