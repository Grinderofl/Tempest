using System;
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
                Arguments = _runnerArguments.GeneratorArguments
            };

            // Load menu
            // Walk through menu, set up options
            // Prepare steps
            generator.Run(context);
        }
    }
}