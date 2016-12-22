using System;
using System.Reflection;
using Tempest.Boot.Conventions.Defaults;
using Tempest.Boot.Strappers.Defaults;
using Tempest.Boot.Strappers.Execution;
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

        //protected virtual void ConfigureBootstrapper(GeneratorContext generatorContext, GeneratorBootstrapper strapper)
        //{
        //    if (generatorContext == null) throw new ArgumentNullException(nameof(generatorContext));
        //    if (strapper == null) throw new ArgumentNullException(nameof(strapper));
        //    var configuration = CreateConfiguration();

        //    // This is kept separate from Bootstrapper because you might want to use your own assemblies.
        //    // You might also want to pass in your own version of generator context
        //    strapper.RegisterConvention(
        //        new MultiAssemblyImplementationRegistrationConvention(
        //            generatorContext.GeneratorType.GetTypeInfo().Assembly));
        //    strapper.RegisterConvention(new GeneratorRegistrationConvention(configuration, generatorContext));
        //}

        

        
    }

    public class GeneratorBootstrapperFactory : IGeneratorBootstrapperFactory
    {
        public virtual GeneratorBootstrapper Create(GeneratorContext generatorContext)
        {
            if (generatorContext == null) throw new ArgumentNullException(nameof(generatorContext));

            var bootstrapper = CreateBootstrapper();
            ConfigureBootstrapper(bootstrapper, generatorContext);
            return bootstrapper;
        }

        protected virtual void ConfigureBootstrapper(GeneratorBootstrapper bootstrapper, GeneratorContext generatorContext)
        {
            if (bootstrapper == null) throw new ArgumentNullException(nameof(bootstrapper));
            if (generatorContext == null) throw new ArgumentNullException(nameof(generatorContext));
            var configuration = CreateConfiguration();

            // This is kept separate from Bootstrapper because you might want to use your own assemblies.
            // You might also want to pass in your own version of generator context
            bootstrapper.RegisterConvention(
                new MultiAssemblyImplementationRegistrationConvention(
                    generatorContext.GeneratorType.GetTypeInfo().Assembly));
            bootstrapper.RegisterConvention(new GeneratorRegistrationConvention(configuration, generatorContext));
        }

        protected virtual GeneratorBootstrapper CreateBootstrapper()
            => new GeneratorBootstrapper();

        protected virtual ScaffoldOperationConfiguration CreateConfiguration()
            => new ScaffoldOperationConfiguration();
    }

    public interface IGeneratorBootstrapperFactory
    {
        GeneratorBootstrapper Create(GeneratorContext generatorContext);
    }
}