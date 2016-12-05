using System;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;
using Tempest.Arguments;
using Tempest.Runner;

namespace Tempest.Boot.Impl
{
    public class CommandLineExecutor : ICommandLineExecutor
    {
        private readonly ITempestRunner _runner;
        private readonly IGeneratorLocator _generatorLocator;
        private readonly IDirectoryFinder _directoryFinder;
        private readonly IArgumentParser _argumentParser;

        public CommandLineExecutor(IArgumentParser argumentParser, ITempestRunner runner, IGeneratorLocator generatorLocator, IDirectoryFinder directoryFinder)
        {
            if (argumentParser == null) throw new ArgumentNullException(nameof(argumentParser));
            if (runner == null) throw new ArgumentNullException(nameof(runner));
            _argumentParser = argumentParser;
            _runner = runner;
            _generatorLocator = generatorLocator;
            _directoryFinder = directoryFinder;
        }

        public virtual int Execute(string[] args)
        {
            var arguments = _argumentParser.ParseArguments(args);
            return ExecuteCore(arguments);
        }

        protected virtual int ExecuteCore(string[] normalisedArguments)
        {
            var application = new CommandLineApplication();
            var generatorName = application.Option(
                "-g | --generator <generatorName>",
                "Name of the generator you want to use",
                CommandOptionType.SingleValue
            );

            var searchPath = application.Option(
                "-s | --search <searchpath>",
                "Path to use to search for generators",
                CommandOptionType.SingleValue
            );

            var generatorArgs = application.Option(
                "-p | --para <generatorArgs>",
                "Arguments to pass to generator",
                CommandOptionType.SingleValue
            );

            var verbosityArgs = application.Option(
                "-v | --verbosity <verbosity>",
                "Verbosity level",
                CommandOptionType.SingleValue
            );

            var runnerArgs = new TempestRunnerArguments();

            application.HelpOption("-? | -h | --help");
            application.OnExecute(() =>
            {
                // Deal with non-running options before
                // That's todo though
                
                if (generatorName.HasValue())
                    runnerArgs.GeneratorName = generatorName.Value();

                if (searchPath.HasValue())
                    runnerArgs.SearchPath = searchPath.Value();

                if (generatorArgs.HasValue())
                    runnerArgs.GeneratorParameters = generatorArgs.Value().Split(' ');

                if (verbosityArgs.HasValue())
                    runnerArgs.Verbosity = verbosityArgs.Value();

                return _runner.Run(runnerArgs);
            });

            if (!normalisedArguments.Any())
            {
                var generators = _generatorLocator.Locate(_directoryFinder.FindGeneratorDirectories().ToArray()).ToArray();
                Console.WriteLine("You haven't picked a generator. Available generators: ");
                int i = 1;
                foreach (var g in generators)
                {
                    Console.WriteLine($"{i}) {g.Name.Replace("Generator", "")}");
                    i++;
                }
                var key = Console.ReadKey();
                var value = int.Parse(key.KeyChar.ToString()) - 1;
                var genName = generators[value].Name.Replace("Generator", "");
                normalisedArguments = new[]
                {
                    "-g",
                    genName
                };
            }

            return application.Execute(normalisedArguments);
        }
    }
}
