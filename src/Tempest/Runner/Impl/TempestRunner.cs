using System;
using System.IO;
using System.Reflection;
using Tempest.Core;

namespace Tempest.Runner.Impl
{
    public class TempestRunner : ITempestRunner
    {
        private readonly IGeneratorLoader _generatorLoader;
        private readonly IDirectoryFinder _directoryFinder;

        public TempestRunner(IGeneratorLoader generatorLoader, IDirectoryFinder directoryFinder)
        {
            if (generatorLoader == null) throw new ArgumentNullException(nameof(generatorLoader));
            _generatorLoader = generatorLoader;
            _directoryFinder = directoryFinder;
        }

        //public void Execute()
        //{
        //    // Runner
        //    // Loads the specified plugin
        //    // Walks through the menu
        //    // Sets up the engine
        //    // Executes the engine

        //    var generator = _generatorLoader.FindGeneratorByName(_runnerArguments.GeneratorName);
        //    if(generator == null)
        //        throw new GeneratorNotFoundException(_runnerArguments.GeneratorName);


        //    // Should probably contain the generator root
        //    // Also current working directory
            


        //    var context = new GeneratorContext()
        //    {
        //        GeneratorName = _runnerArguments.GeneratorName,
        //        Arguments = _runnerArguments.GeneratorParameters,
        //        TempestDirectory = GetCurrentDirectory(),
        //        WorkingDirectory = GetWorkingDirectory()
        //    };

        //    // Load menu
        //    // Walk through menu, set up options
        //    // Prepare steps
        //    generator.Run(context);
        //}

        //private DirectoryInfo GetWorkingDirectory()
        //{
        //    return new DirectoryInfo(Directory.GetCurrentDirectory());
        //}

        //private DirectoryInfo GetCurrentDirectory()
        //{
        //    var codeBase = typeof(TempestRunner).GetTypeInfo().Assembly.CodeBase;
        //    var builder = new UriBuilder(codeBase);
        //    var filePath = Uri.UnescapeDataString(builder.Path);
        //    var path = Path.GetDirectoryName(filePath);

        //    return new DirectoryInfo(path);
        //}

        public int Run(TempestRunnerArguments runnerArgs)
        {
            var loaderContext = new LoaderContext()
            {
                Name = runnerArgs.GeneratorName,
                AdditionalSearchPath = runnerArgs.SearchPath,
            };
            var generator = _generatorLoader.Load(loaderContext); //LoadGenerator(runnerArgs.SearchPath);
            var generatorContext = new GeneratorContext()
            {
                Arguments = runnerArgs.GeneratorParameters,
                GeneratorName = runnerArgs.GeneratorName,
                // GeneratorDirectory = 
                WorkingDirectory = _directoryFinder.FindWorkingDirectory(),
                TempestDirectory = _directoryFinder.FindTempestExecutableDirectory()
            };
            generator.Run(generatorContext);
            return 0;
            // Find generator
            // Execute generator
        }
    }
}