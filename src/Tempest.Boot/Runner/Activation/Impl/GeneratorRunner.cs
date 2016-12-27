using System;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core;
using Tempest.Core.Generator;

namespace Tempest.Boot.Runner.Activation.Impl
{
    public class GeneratorRunner : IGeneratorRunner
    {
        private readonly IGeneratorBootstrapperFactory _bootstrapperFactory;

        public GeneratorRunner(IGeneratorBootstrapperFactory bootstrapperFactory)
        {
            _bootstrapperFactory = bootstrapperFactory;
        }

        public virtual int Run(GeneratorContext generatorContext)
        {
            if (generatorContext == null) throw new ArgumentNullException(nameof(generatorContext));
            var strapper = _bootstrapperFactory.Create(generatorContext);
            return strapper.Execute(new GeneratorExecutor());
        }
        
    }
}