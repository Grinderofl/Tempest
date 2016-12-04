using System;
using System.IO;
using System.Reflection;

namespace Tempest.Runner
{
    public interface IGeneratorLocator
    {
        Type Locate(DirectoryInfo[] directoriesToSearch, string generatorName);
    }
}