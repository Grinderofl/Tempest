using Tempest.Boot.Strappers.Defaults;
using Tempest.Core;
using Tempest.Core.Generator;

namespace Tempest.Boot.Runner.Activation
{
    public interface IGeneratorBootstrapperFactory
    {
        GeneratorBootstrapper Create(GeneratorContext generatorContext);
    }
}