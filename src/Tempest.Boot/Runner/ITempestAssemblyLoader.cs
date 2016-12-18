using System.Reflection;

namespace Tempest.Boot.Runner
{
    public interface ITempestAssemblyLoader
    {
        Assembly Load(string path);
    }
}