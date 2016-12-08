using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot;
using Tempest.Boot.Conventions;
using Tempest.Core;

namespace Tempest.Runner.Impl
{
    public class GeneratorActivator : IGeneratorActivator
    {
        public GeneratorEngineBase Activate(Type generatorType)
        {
            //var services = new ServiceCollection();
            //services.AddSingleton<IServiceConfigurationConvention>(new RegisterAbstractImplementations(generatorType));

            return (GeneratorEngineBase)Activator.CreateInstance(generatorType);
        }
    }
}
