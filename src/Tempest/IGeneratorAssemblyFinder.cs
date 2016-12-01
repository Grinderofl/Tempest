using System.Reflection;

namespace Tempest
{
    public interface IGeneratorAssemblyFinder
    {
        Assembly FindByName(string generatorName);
    }
}