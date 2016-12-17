using System.IO;

namespace Tempest.Core.Scaffolding.Sources
{
    public interface IStreamSource
    {
        string Describe();
        Stream Create();
    }
}