using System.Collections.Generic;
using System.Reflection;
using Tempest.Boot.Runner.Impl;

namespace Tempest.Boot.Conventions
{
    public class RegisterDefaultImplementations : RegisterImplementations
    {
        protected override IEnumerable<Assembly> IncludedAssemblies()
        {
            yield return typeof(TempestRunner).GetTypeInfo().Assembly;
        }
    }
}