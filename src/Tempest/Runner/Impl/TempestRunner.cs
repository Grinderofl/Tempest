using System;
using System.Linq;
using System.Reflection;
using Tempest.Core;
using Tempest.Core.Utils;

namespace Tempest.Runner.Impl
{
    public class TempestRunner : ITempestRunner
    {
        private readonly IDirectoryFinder _directoryFinder;
        private readonly IGeneratorLocator _generatorLocator;
        private readonly IGeneratorLoader _generatorLoader;

        public TempestRunner(IGeneratorLoader generatorLoader, IDirectoryFinder directoryFinder, IGeneratorLocator generatorLocator)
        {
            if (generatorLoader == null) throw new ArgumentNullException(nameof(generatorLoader));
            if (directoryFinder == null) throw new ArgumentNullException(nameof(directoryFinder));
            if (generatorLocator == null) throw new ArgumentNullException(nameof(generatorLocator));
            _generatorLoader = generatorLoader;
            _directoryFinder = directoryFinder;
            _generatorLocator = generatorLocator;
        }

        public int Run(TempestRunnerArguments runnerArgs)
        {
            Type generatorType = null;

            if (runnerArgs.GeneratorName.IsNotNullOrWhiteSpace())
            {
                generatorType =
                    _generatorLocator.Locate(
                        _directoryFinder.FindGeneratorDirectories(runnerArgs.SearchPath).ToArray(),
                        runnerArgs.GeneratorName);
            }
            if (generatorType == null && runnerArgs.GeneratorName.IsNullOrWhiteSpace())
            {
                generatorType = GetGeneratorFromSelection(runnerArgs);
            }
            if (generatorType == null)
                throw new GeneratorNotFoundException("No generators found");
            
            var loaderContext = new LoaderContext
            {
                Type = generatorType,
                Name = generatorType.Name,
                AdditionalSearchPath = runnerArgs.SearchPath
            };
            var generator = _generatorLoader.Load(loaderContext);
            var generatorContext = new GeneratorContext
            {
                Arguments = runnerArgs.GeneratorParameters,
                GeneratorName = runnerArgs.GeneratorName,
                TemplateDirectory = generatorType.GetAssembly().GetAssemblyDirectory().GetDirectories("Template").FirstOrDefault() ?? generatorType.GetAssembly().GetAssemblyDirectory(),
                WorkingDirectory = _directoryFinder.FindWorkingDirectory(),
                TempestDirectory = _directoryFinder.FindTempestExecutableDirectory()
            };
            generator.Run(generatorContext);
            return 0;
        }

        protected Type GetGeneratorFromSelection(TempestRunnerArguments runnerArguments)
        {
            var generators = _generatorLocator.Locate(_directoryFinder.FindGeneratorDirectories(runnerArguments.SearchPath).ToArray()).ToArray();
            Console.WriteLine("You haven't picked a generator. Available generators: ");
            var i = 1;
            foreach (var g in generators)
            {
                Console.WriteLine($"{i}) {g.Name.Replace("Generator", "")}");
                i++;
            }
            
            int? value = null;

            Type generator = null;
            while (value == null)
            {
                try
                {
                    var key = Console.ReadKey();
                    value = int.Parse(key.KeyChar.ToString()) - 1;
                    generator = generators[value.Value];
                }
                catch (Exception)
                {
                    // Catch until we get an entry
                }
            }
            
            return generator;
        }

    }
}