using Tempest.Core;

namespace Tempest
{
    public interface IGeneratorLoader
    {
        EngineBase FindGeneratorByName(string generatorName);
    }
}