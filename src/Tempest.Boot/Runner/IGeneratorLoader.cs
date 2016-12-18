using Tempest.Core;

namespace Tempest.Boot.Runner
{
    public interface IGeneratorLoader
    {
        GeneratorEngineBase Load(LoaderContext loaderContext);
    }
}