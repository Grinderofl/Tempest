using Tempest.Boot.Runner.Impl;
using Tempest.Core;

namespace Tempest.Boot.Runner
{
    public interface IScaffolderRunner
    {
        //GeneratorEngineBase Create(LoaderContext loaderContext);
        int Run(GeneratorContext loaderContext);
    }
}