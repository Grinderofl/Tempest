using System.Reflection;

namespace Tempest.Boot.Runner.Activation
{
    public interface ITempestAssemblyLoader
    {
        Assembly Load(string path);
    }
}