using System;
using Tempest.Core;

namespace Tempest.Boot.Runner.Impl
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
