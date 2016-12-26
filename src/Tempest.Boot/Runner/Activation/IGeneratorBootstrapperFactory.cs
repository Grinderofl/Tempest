using Tempest.Boot.Strappers.Defaults;
using Tempest.Core;

namespace Tempest.Boot.Runner.Activation
{
    public interface IGeneratorBootstrapperFactory
    {
        GeneratorBootstrapper Create(GeneratorContext generatorContext);
    }
}