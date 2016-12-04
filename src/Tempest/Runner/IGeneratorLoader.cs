using Tempest.Core;
using Tempest.Runner.Impl;

namespace Tempest.Runner
{
    public interface IGeneratorLoader
    {
        //GeneratorEngineBase FindGeneratorByName(string generatorName);
        GeneratorEngineBase Load(LoaderContext loaderContext);
    }
}