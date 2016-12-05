using Tempest.Core;

namespace Tempest.Runner
{
    public interface IGeneratorLoader
    {
        GeneratorEngineBase Load(LoaderContext loaderContext);
    }
}