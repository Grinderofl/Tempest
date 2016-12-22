using System;
using Microsoft.Extensions.CommandLineUtils;
using Tempest.Boot.Configuration;

namespace Tempest.Boot.Runner.Impl
{

    // TODO extract commands
    public class CommandLineExecutor : ICommandLineExecutor
    {
        private readonly IArgumentParser _argumentParser;
        private readonly ITempestRunner _runner;

        public CommandLineExecutor(IArgumentParser argumentParser, ITempestRunner runner)
        {
            if (argumentParser == null) throw new ArgumentNullException(nameof(argumentParser));
            if (runner == null) throw new ArgumentNullException(nameof(runner));
            _argumentParser = argumentParser;
            _runner = runner;
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

            
            application.HelpOption("-? | -h | --help");
            application.OnExecute(() =>
            {
                var runnerArgs = new TempestRunnerArguments();
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

            return application.Execute(normalisedArguments);
        }
    }
}