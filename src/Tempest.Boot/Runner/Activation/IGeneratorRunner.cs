using Tempest.Core;
using Tempest.Core.Generator;

namespace Tempest.Boot.Runner.Activation
{
    public interface IGeneratorRunner
    {
        int Run(GeneratorContext generatorContext);
    }
}