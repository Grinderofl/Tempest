using System;
using Tempest.Core;

namespace Tempest.Runner.Impl
{
    public class TempestRunner : ITempestRunner
    {
        private readonly IDirectoryFinder _directoryFinder;
        private readonly IGeneratorLoader _generatorLoader;

        public TempestRunner(IGeneratorLoader generatorLoader, IDirectoryFinder directoryFinder)
        {
            if (generatorLoader == null) throw new ArgumentNullException(nameof(generatorLoader));
            if (directoryFinder == null) throw new ArgumentNullException(nameof(directoryFinder));
            _generatorLoader = generatorLoader;
            _directoryFinder = directoryFinder;
        }

        public int Run(TempestRunnerArguments runnerArgs)
        {
            var loaderContext = new LoaderContext
            {
                Name = runnerArgs.GeneratorName,
                AdditionalSearchPath = runnerArgs.SearchPath
            };
            var generator = _generatorLoader.Load(loaderContext);
            var generatorContext = new GeneratorContext
            {
                Arguments = runnerArgs.GeneratorParameters,
                GeneratorName = runnerArgs.GeneratorName,
                // GeneratorDirectory = 
                WorkingDirectory = _directoryFinder.FindWorkingDirectory(),
                TempestDirectory = _directoryFinder.FindTempestExecutableDirectory()
            };
            generator.Run(generatorContext);
            return 0;
        }
    }
}