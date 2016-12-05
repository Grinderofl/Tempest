using Tempest.Core;
using Tempest.Runner.Impl;

namespace Tempest.Runner
{
    public interface IGeneratorLoader
    {
        GeneratorEngineBase Load(LoaderContext loaderContext);
    }
}