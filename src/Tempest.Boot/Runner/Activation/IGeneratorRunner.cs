using Tempest.Core;

namespace Tempest.Boot.Runner.Activation
{
    public interface IGeneratorRunner
    {
        int Run(GeneratorContext generatorContext);
    }
}