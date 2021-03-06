using System;
using System.Reflection;
using Tempest.Boot.Conventions;
using Tempest.Boot.Strappers.Defaults;
using Tempest.Core;
using Tempest.Core.Conventions.Defaults;
using Tempest.Core.Generator;
using Tempest.Core.Operations;
using Tempest.Core.Utils;

namespace Tempest.Boot.Runner.Activation.Impl
{
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
                    generatorContext.GeneratorType.GetAssembly()));
            bootstrapper.RegisterConvention(new GeneratorRegistrationConvention(configuration, generatorContext));
        }

        protected virtual GeneratorBootstrapper CreateBootstrapper()
            => new GeneratorBootstrapper();

        protected virtual ScaffoldOperationConfiguration CreateConfiguration()
            => new ScaffoldOperationConfiguration();
    }
}