using System.Collections.Generic;
using System.Threading;
using Tempest.Arguments;

namespace Tempest
{
    public class Program
    {


        public static void Main(string[] args)
        {
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

            var arguments = new RunnerArgumentFactory().Create(args);
            var runner = BuildRunner(arguments);
            runner.Execute();
        }

        private static TempestRunner BuildRunner(RunnerArguments arguments)
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

