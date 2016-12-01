using System.Collections.Generic;
using System.Threading;

namespace Tempest
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

