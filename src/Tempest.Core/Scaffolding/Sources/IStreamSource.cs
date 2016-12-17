using System.IO;

namespace Tempest.Core.Scaffolding.Sources
{
    public interface IStreamSource
    {
        Stream Create();
    }
}