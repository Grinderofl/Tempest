using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Boot.Configuration;
using Tempest.Boot.Helpers;
using Tempest.Boot.Runner.Activation;
using Tempest.Boot.Utils;
using Tempest.Core;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Scaffolding.Impl;
using Tempest.Core.Generator;
using Tempest.Core.Options.Impl;
using Tempest.Core.Options.Rendering;
using Tempest.Core.Utils;

namespace Tempest.Boot.Runner.Impl
{
    // TODO This does waaay too much :D
    // edit: fixed 8)
    public class TempestRunner : ITempestRunner
    {
        private readonly IDirectoryFinder _directoryFinder;
        private readonly IGeneratorFinder _generatorFinder;
        private readonly IGeneratorRunner _generatorRunner;
        private readonly OptionsFactory _optionsFactory = new OptionsFactory();

        public TempestRunner(IGeneratorRunner generatorRunner, IDirectoryFinder directoryFinder, IGeneratorFinder generatorFinder)
        {
            if (generatorRunner == null) throw new ArgumentNullException(nameof(generatorRunner));
            if (directoryFinder == null) throw new ArgumentNullException(nameof(directoryFinder));
            if (generatorFinder == null) throw new ArgumentNullException(nameof(generatorFinder));
            _generatorRunner = generatorRunner;
            _directoryFinder = directoryFinder;
            _generatorFinder = generatorFinder;
        }

        public virtual int Run(TempestRunnerArguments runnerArgs)
        {
            Type generatorType = null;

            if (runnerArgs.GeneratorName.IsNotNullOrWhiteSpace())
            {
                var directoriesToSearch = _directoryFinder.FindGeneratorDirectories(runnerArgs.SearchPath).ToArray();
                generatorType = _generatorFinder.LocateGenerator(directoriesToSearch, runnerArgs.GeneratorName);
            }

            if (generatorType == null && runnerArgs.GeneratorName.IsNullOrWhiteSpace())
                generatorType = GetGeneratorFromSelection(runnerArgs);

            if (generatorType == null)
                throw new GeneratorNotFoundException("No generators found");

            var templateDirectory = generatorType.GetAssembly()
                                        .GetAssemblyDirectory()
                                        .GetDirectories("Template")
                                        .FirstOrDefault() ??
                                    generatorType.GetAssembly().GetAssemblyDirectory();

            var generatorContext = GeneratorContextFactory.Create(generatorType,
                _directoryFinder.FindTempestExecutableDirectory(),
                _directoryFinder.FindWorkingDirectory(), templateDirectory, x =>
                {
                    x.Arguments = runnerArgs.GeneratorParameters;
                    x.GeneratorName = runnerArgs.GeneratorName;
                });

//            var generatorContext = new GeneratorContext
//            {
//                GeneratorType = generatorType,
//                Arguments = runnerArgs.GeneratorParameters,
//                GeneratorName = runnerArgs.GeneratorName,
//                TemplateDirectory = generatorType.GetAssembly().GetAssemblyDirectory().GetDirectories("Template").FirstOrDefault() ?? generatorType.GetAssembly().GetAssemblyDirectory(),
//                WorkingDirectory = _directoryFinder.FindWorkingDirectory(),
//                TempestDirectory = _directoryFinder.FindTempestExecutableDirectory()
//            };

            return _generatorRunner.Run(generatorContext);

        }

        // Should be delegated to another service maybe
        protected Type GetGeneratorFromSelection(TempestRunnerArguments runnerArguments)
        {
            var generators = _generatorFinder.LocateGenerators(_directoryFinder.FindGeneratorDirectories(runnerArguments.SearchPath).ToArray()).ToArray();
            var options = _optionsFactory.List("You haven't picked a generator. Available generators:");
            var generatorMap = new Dictionary<string, Type>();
            foreach (var generatorType in generators)
            {
                var generatorName = generatorType.Name.Replace("Generator", "");
                generatorMap.Add(generatorName.ToLower(), generatorType);
                options.Choice($"{generatorName}", generatorName.ToLower());
            }
            var generatorChoice = ((IConfigurationOption) options).Render(new RenderContext(new RenderOptions()));
            var generator = generatorMap[generatorChoice];
            return generator;
        }

    }
}