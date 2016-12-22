using System;
using System.Collections.Generic;
using System.IO;

namespace Tempest.Boot.Runner.Activation
{
    public interface IGeneratorLocator
    {
        Type Locate(DirectoryInfo[] directoriesToSearch, string generatorName);
        IEnumerable<Type> Locate(DirectoryInfo[] directoriesToSearch);
    }
}