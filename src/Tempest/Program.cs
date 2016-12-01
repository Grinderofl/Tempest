using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Tempest.Core;

namespace Tempest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var arguments = new RunnerArgumentFactory().Create(args);
            var directoryFinder = new DirectoryFinder();
            var runner = new TempestRunner(arguments);

        }
    }


    
    


    public class RunnerArgumentFactory
    {
        public RunnerArguments Create(string[] args)
        {
            if (args[0] != null)
            {
                var generatorArgs = args.Skip(1).ToArray();
                return new RunnerArguments()
                {
                    Generator = args[0],
                    GeneratorArguments = generatorArgs
                };
            }
            return new RunnerArguments();
        }
    }

    public class RunnerArguments
    {
        public string Generator { get; set; }
        public string[] GeneratorArguments { get; set; }
    }

    public class TempestRunner
    {
        private RunnerArguments _runnerArguments;
        private IDirectoryFinder _directoryFinder;

        public TempestRunner(RunnerArguments runnerArguments)
        {
            if (runnerArguments == null) throw new ArgumentNullException(nameof(runnerArguments));
            _runnerArguments = runnerArguments;
        }

        public void Execute()
        {
            // Runner
            // Loads the specified plugin
            // Walks through the menu
            // Sets up the engine
            // Executes the engine



        }
    }


    public class GeneratorLoader
    {
        // Generator loader
        // Finds the DLL with the generator name
        // Loads the DLL
        // Loads the Generator type
        private static readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

        public Generator FindGeneratorByName(string name)
        {
            
        }


    }

    public class DirectoryFinder : IDirectoryFinder
    {
        public DirectoryInfo FindGeneratorLibraryDirectory()
        {
            var codeBase = typeof(TempestRunner).GetTypeInfo().Assembly.CodeBase;
            var builder = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(builder.Path);
            if(Directory.Exists(path))
                return new DirectoryInfo(path);
            return null;
        }
    }

    public interface IDirectoryFinder
    {
        DirectoryInfo FindGeneratorLibraryDirectory();
    }

    public interface IGeneratorFinder
    {
        Generator FindByName(string name);
    }

    public class GeneratorFinder : IGeneratorFinder
    {
        private IDirectoryFinder _directoryFinder;

        public GeneratorFinder(IDirectoryFinder directoryFinder)
        {
            if (directoryFinder == null) throw new ArgumentNullException(nameof(directoryFinder));
            _directoryFinder = directoryFinder;
        }

        public Generator FindByName(string name)
        {
            var assembly = FindGeneratorAssembly(name);
        }

        protected Assembly FindGeneratorAssembly(string name)
        {
            var generatorDirectory = FindGeneratorDirectory(name);

        }

        private string FindGeneratorDirectory(string name)
        {
            var generatorsDirectory = _directoryFinder.FindGeneratorLibraryDirectory();
            var generatorNamePattern = $"Tempest.Generator.{name}";
            var generatorDirectory = Path.Combine(generatorsDirectory.FullName, generatorNamePattern);
            if (Directory.Exists(generatorDirectory))
            {
                throw new DirectoryNotFoundException(
                    $"The plugin {name} directory could not be found. Searched location: '{generatorDirectory}'");
            }
            return generatorDirectory;
        }
    }
}

