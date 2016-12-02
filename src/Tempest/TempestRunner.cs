using System;
using System.IO;
using System.Reflection;
using Microsoft.DotNet.PlatformAbstractions;
using Tempest.Arguments;
using Tempest.Core;

namespace Tempest
{
    public class TempestRunner
    {
        private readonly RunnerArguments _runnerArguments;
        private readonly IGeneratorLoader _generatorLoader;

        public TempestRunner(RunnerArguments runnerArguments, IGeneratorLoader generatorLoader)
        {
            if (runnerArguments == null) throw new ArgumentNullException(nameof(runnerArguments));
            _runnerArguments = runnerArguments;
            _generatorLoader = generatorLoader;
        }

        public void Execute()
        {
            // Runner
            // Loads the specified plugin
            // Walks through the menu
            // Sets up the engine
            // Executes the engine

            var generator = _generatorLoader.FindGeneratorByName(_runnerArguments.Generator);
            if(generator == null)
                throw new GeneratorNotFoundException(_runnerArguments.Generator);


            // Should probably contain the generator root
            // Also current working directory
            
            var context = new RunnerContext()
            {
                GeneratorName = _runnerArguments.Generator,
                Arguments = _runnerArguments.GeneratorArguments,
                TempestDirectory = GetCurrentDirectory(),
                WorkingDirectory = GetWorkingDirectory()
            };

            // Load menu
            // Walk through menu, set up options
            // Prepare steps
            generator.Run(context);
        }

        private DirectoryInfo GetWorkingDirectory()
        {
            return new DirectoryInfo(Directory.GetCurrentDirectory());
        }

        private DirectoryInfo GetCurrentDirectory()
        {
            var codeBase = typeof(TempestRunner).GetTypeInfo().Assembly.CodeBase;
            var builder = new UriBuilder(codeBase);
            var filePath = Uri.UnescapeDataString(builder.Path);
            var path = Path.GetDirectoryName(filePath);

            return new DirectoryInfo(path);
        }
    }
}