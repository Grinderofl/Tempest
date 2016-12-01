using Tempest.Core;

namespace Tempest
{
    public interface IGeneratorLoader
    {
        GeneratorEngineBase FindGeneratorByName(string generatorName);
    }
}