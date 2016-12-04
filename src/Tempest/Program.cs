using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Arguments;

namespace Tempest
{
    public class Program
    {


        public static int Main(string[] args)
        {
            var semanticArguments = SemanticArgumentParser.Parse(args);


            // Should support following arguments:
            // 
            // -i | --install [<PackageName>|<PackageName.zip>]
            //      --update all

            // -u | --uninstall <PackageName>
            // -s | --search <SearchPath>                   Configures the directory to search for generators
            // -a | --add-search <SearchPath>               Adds a default path to search for generators
            // -r | --remove-search <SearchPath>            Removes a default path from being searched for generators
            // -l | --list [Generators|Search]              Lists all generators or search paths
            // -v | --verbosity <VerbosityLevel>            Specifies the verbosity level
            // -p | --para <Parameters>                     Specifies the generator parameters
            // -g | --generator <Name>                      Specifies the generator to use

            // Syntax:
            // tempest <generatorName> -iusarlvpg <command>
            // tempest <generatorName> <Parameters> -iusarlvpg <command>
            // tempest <generatorName> -iusarlvpg <command>

            // tempest -i <command> -u <command> -s <command> <generatorName> <Parameters>
            // tempest <generatorName> <Parameters> -i <command> -u <command> -s <command>
            // tempest -iusarlvpg command
            // tempest -iusarlvpg command <generatorName>
            // tempest -iusarlvpg command <generatorName> <Parameters>

            // What's the pattern here?
            // If we assume that an argument is always in a form "-X" or "--Xxxxx", and always follows by a single word, or something between " and " ... maybe we can extract it like that?

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
                if (generatorName.HasValue())
                    runnerArgs.GeneratorName = generatorName.Value();

                if (searchPath.HasValue())
                    runnerArgs.SearchPath = searchPath.Value();

                if (generatorArgs.HasValue())
                    runnerArgs.GeneratorParameters = generatorArgs.Value().Split(' ');

                if (verbosityArgs.HasValue())
                    runnerArgs.Verbosity = verbosityArgs.Value();

                Console.ReadKey();
                var strapper = TempestBootstrapper.CreateDefault();
                return strapper.Strap(runnerArgs);
            });
            application.Execute(semanticArguments);
            Console.ReadKey();



            return 0;

        }
    }

    public class TempestRunnerArguments
    {
        public string GeneratorName { get; set; }
        public string[] GeneratorParameters { get; set; }
        public string SearchPath { get; set; }
        public string Verbosity { get; set; }
    }

    public class SemanticArgumentParser
    {
        public static string[] Parse(string[] args)
        {
            var isSemanticContext = true;
            var justAddedCommandArgument = false;

            var semanticArgs = new List<string>();
            var commandArgs = new List<string>();

            foreach (var argument in args)
            {
                if (argument.StartsWith("-"))
                    isSemanticContext = false;
                else if (justAddedCommandArgument)
                    isSemanticContext = true;


                if (isSemanticContext)
                {
                    semanticArgs.Add(argument);
                    justAddedCommandArgument = false;
                }
                else
                {
                    commandArgs.Add(argument);
                    // Allow insertion of a single '-' argument
                    // and its single parameter
                    if (!argument.StartsWith("-"))
                        justAddedCommandArgument = true;
                }
            }

            if (semanticArgs.Any())
            {
                commandArgs.Add("-g");
                commandArgs.Add(semanticArgs.First());

                if (semanticArgs.Count > 1)
                {
                    commandArgs.Add("-p");
                    commandArgs.Add($"{string.Join(" ", semanticArgs.Skip(1))}");
                }
            }



            return commandArgs.ToArray();
        }
    }

    public abstract class TempestBootstrapper
    {
        public static TempestBootstrapper<IServiceCollection> CreateDefault()
        {
            return TempestBootstrapper<IServiceCollection>.Create(new DefaultServiceProviderFactory());
        }

    }

    public class TempestBootstrapper<T> : TempestBootstrapper
    {
        private readonly IServiceProviderFactory<T> _serviceProviderFactory;

        public static TempestBootstrapper<T2> Create<T2>(IServiceProviderFactory<T2> factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return new TempestBootstrapper<T2>(factory);
        }
        
        protected TempestBootstrapper(IServiceProviderFactory<T> serviceProviderFactory)
        {
            _serviceProviderFactory = serviceProviderFactory;
        }

        protected virtual IServiceProvider CreateProvider(IServiceProviderFactory<T> factory)
        {
            var services = new ServiceCollection();
            ConfigureLocalServices(services);
            var builder = factory.CreateBuilder(services);
            return factory.CreateServiceProvider(builder);
        }

        protected virtual void ConfigureLocalServices(IServiceCollection services)
        {
        }

        public int Strap(TempestRunnerArguments args)
        {
            var container = CreateProvider(_serviceProviderFactory);

            // Resolve what?

            //var arguments = new RunnerArgumentFactory().Create(args);
            var runner = BuildRunner(args);
            runner.Execute();
            return 0;
        }

        private static TempestRunner BuildRunner(TempestRunnerArguments arguments)
        {
            GeneratorLoader loader = BuildGeneratorLoader();
            var runner = new TempestRunner(arguments, loader);
            return runner;
        }

        private static GeneratorLoader BuildGeneratorLoader()
        {
            var directoryFinder = new DirectoryFinder();
            var assemblyFinder = new GeneratorAssemblyFinder(directoryFinder);
            var loader = new GeneratorLoader(assemblyFinder);
            return loader;
        }
    }
}

