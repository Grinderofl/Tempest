using System;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions;
using Tempest.Boot.Runner.Impl;

namespace Tempest.Boot.Strapper
{
    public class GeneratorBootstrapper : ConventionTempestBootstrapper
    {
        private readonly Type _generatorType;

        public GeneratorBootstrapper(Type generatorType) : base(new DefaultServiceProviderFactory())
        {
            _generatorType = generatorType;
        }

        public virtual void RegisterConvention(IServiceConfigurationConvention convention)
        {
            AddConvention(convention);
        }

        protected override void ConfigureConventions()
        {
            AddConvention(new RegisterAbstractImplementations(_generatorType));
        }

        public override int Execute(string[] args)
        {
            var provider = CreateProvider();
            var executor = provider.GetService<IGeneratorExecutor>();
            executor.Execute();
            return 0;
        }
    }
}