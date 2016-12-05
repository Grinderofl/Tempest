using System;
using System.Collections.Generic;
using System.IO;

namespace Tempest.Runner
{
    public interface IGeneratorLocator
    {
        Type Locate(DirectoryInfo[] directoriesToSearch, string generatorName);
        IEnumerable<Type> Locate(DirectoryInfo[] directoriesToSearch);
    }
}