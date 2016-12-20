using Tempest.Boot.Runner.Impl;
using Tempest.Core;

namespace Tempest.Boot.Runner
{
    public interface IGeneratorRunner
    {
        //GeneratorEngineBase Create(LoaderContext loaderContext);
        int Run(GeneratorContext loaderContext);
    }
}