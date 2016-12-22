using System;
using System.Reflection;
using Tempest.Boot.Conventions.Defaults;
using Tempest.Boot.Strappers.Defaults;
using Tempest.Core;
using Tempest.Core.Operations;

namespace Tempest.Boot.Runner.Activation.Impl
{
    // Tempest Runner does this: parses initial arguments, and searches for the generator
    // It then passes the entire shabang onto a generator executor
    // executor creates a basic bootstrapper and adds core and the assembly of
    // the generator as targets for service discovery
    // The relevant parameters are also added as services
    // then bootstrapper executes the generator itself

    // This will run scaffolder

    public class GeneratorRunner : IGeneratorRunner
    {
        public virtual int Run(GeneratorContext generatorContext)
        {
            if (generatorContext == null) throw new ArgumentNullException(nameof(generatorContext));
            var strapper = CreateBootstrapper();
            ConfigureBootstrapper(generatorContext, strapper);

            return strapper.Execute(null);
        }

        protected virtual void ConfigureBootstrapper(GeneratorContext generatorContext, GeneratorBootstrapper strapper)
        {
            if (generatorContext == null) throw new ArgumentNullException(nameof(generatorContext));
            if (strapper == null) throw new ArgumentNullException(nameof(strapper));
            var configuration = CreateConfiguration();

            // This is kept separate from Bootstrapper because you might want to use your own assemblies.
            // You might also want to pass in your own version of generator context
            strapper.RegisterConvention(
                new MultiAssemblyImplementationRegistrationConvention(
                    generatorContext.GeneratorType.GetTypeInfo().Assembly));
            strapper.RegisterConvention(new GeneratorRegistrationConvention(configuration, generatorContext));
        }

        protected virtual GeneratorBootstrapper CreateBootstrapper()
            => new GeneratorBootstrapper();

        protected virtual ScaffoldOperationConfiguration CreateConfiguration() 
            => new ScaffoldOperationConfiguration();

    }
}