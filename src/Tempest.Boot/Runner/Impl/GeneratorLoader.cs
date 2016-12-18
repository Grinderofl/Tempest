using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tempest.Core;

namespace Tempest.Boot.Runner.Impl
{
    public class GeneratorLoader : IGeneratorLoader
    {
        private readonly IGeneratorActivator _activator;
        private readonly IDirectoryFinder _directoryFinder;
        private readonly IGeneratorLocator _locator;

        public GeneratorLoader(IDirectoryFinder directoryFinder, IGeneratorLocator locator, IGeneratorActivator activator)
        {
            if (directoryFinder == null) throw new ArgumentNullException(nameof(directoryFinder));
            if (locator == null) throw new ArgumentNullException(nameof(locator));
            _directoryFinder = directoryFinder;
            _locator = locator;
            _activator = activator;
        }

        public GeneratorEngineBase Load(LoaderContext loaderContext)
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
    }
}