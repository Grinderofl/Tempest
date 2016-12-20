using System;
using System.Linq;
using Tempest.Core;
using Tempest.Core.Utils;

namespace Tempest.Boot.Runner.Impl
{
    // TODO This does waaay too much :D
    public class TempestRunner : ITempestRunner
    {
        private readonly IDirectoryFinder _directoryFinder;
        private readonly IGeneratorLocator _generatorLocator;
        private readonly IGeneratorRunner _generatorRunner;

        public TempestRunner(IGeneratorRunner generatorRunner, IDirectoryFinder directoryFinder, IGeneratorLocator generatorLocator)
        {
            if (generatorRunner == null) throw new ArgumentNullException(nameof(generatorRunner));
            if (directoryFinder == null) throw new ArgumentNullException(nameof(directoryFinder));
            if (generatorLocator == null) throw new ArgumentNullException(nameof(generatorLocator));
            _generatorRunner = generatorRunner;
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

            // GeneratorExecutorFactory puts together all the dependency stuff then?
            // It also retrieves the executor from service provider

            var generatorContext = new GeneratorContext
            {
                GeneratorType = generatorType,
                Arguments = runnerArgs.GeneratorParameters,
                GeneratorName = runnerArgs.GeneratorName,
                TemplateDirectory = generatorType.GetAssembly().GetAssemblyDirectory().GetDirectories("Template").FirstOrDefault() ?? generatorType.GetAssembly().GetAssemblyDirectory(),
                WorkingDirectory = _directoryFinder.FindWorkingDirectory(),
                TempestDirectory = _directoryFinder.FindTempestExecutableDirectory()
            };

            return _generatorRunner.Run(generatorContext);
            
            ////generatorExecutor.Run(generatorContext);
            //var executionResult = generatorExecutor.Execute(generatorContext);

            //foreach (var operation in executionResult.Operations)
            //    operation.Execute();

            //return 0;
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