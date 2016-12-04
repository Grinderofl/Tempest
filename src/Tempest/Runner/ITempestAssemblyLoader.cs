using System.Reflection;

namespace Tempest.Runner
{
    public interface ITempestAssemblyLoader
    {
        Assembly Load(string path);
    }
}